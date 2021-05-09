using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.Data.Decorators
{
    public class TransactionCommandDecorator<TCommand> : CommandHandlerDecorator<TCommand> 
        where TCommand : class, ICommand
    {
        private static readonly string ActionName = $"{nameof(TransactionCommandDecorator<TCommand>)}<{typeof(TCommand).Name}>";
        private static readonly string SavepointName = typeof(TCommand).Name;

        private readonly ILogger<TransactionCommandDecorator<TCommand>> _logger;
        private readonly DbContext _dbContext;

        private bool _isNestedTransaction;
        
        public TransactionCommandDecorator(
            DbContext dbContext, 
            ICommandHandler<TCommand> innerHandler,
            ILogger<TransactionCommandDecorator<TCommand>> logger) 
            : base(innerHandler)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var transaction = await CreateTransactionAsync(cancellationToken);

            // ReSharper disable once MethodHasAsyncOverload
            var result = Result.Failed(500, "Transaction decorator throw an exception.");

            try
            {
                result = await InnerHandler.HandleAsync(command, cancellationToken);
            }
            finally
            {
                if (result.IsFailed == false) 
                    await CommitTransactionAsync(transaction, cancellationToken);
                else
                    await RollbackTransactionAsync(transaction, cancellationToken);
            }
            
            return result;
        }

        private async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken)
        {
            _isNestedTransaction = _dbContext.Database.CurrentTransaction is not null;

            var transaction = _isNestedTransaction
                ? _dbContext.Database.CurrentTransaction
                : await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            if (_isNestedTransaction)
                _logger.LogInformation("Action='{ActionName}' Message='Transaction already exist.' TransactionId='{TransactionId}'", ActionName, transaction!.TransactionId);
            else
                _logger.LogInformation("Action='{ActionName}' Message='Transaction created.' TransactionId='{TransactionId}'", ActionName, transaction!.TransactionId);
            
            await transaction!.CreateSavepointAsync(SavepointName, cancellationToken);
            _logger.LogInformation("Action='{ActionName}' Message='Savepoint created.' TransactionId='{TransactionId}' Savepoint='{Savepoint}'", ActionName, transaction!.TransactionId, SavepointName);

            return transaction;
        }

        private async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            if (_isNestedTransaction)
            {
                _logger.LogInformation("Action='{ActionName}' Message='Is nested transaction. Commit action skipped.' TransactionId='{TransactionId}'", ActionName, transaction!.TransactionId);
                return;
            }
            
            await transaction.CommitAsync(cancellationToken);
            await transaction.DisposeAsync();
            
            _logger.LogInformation("Action='{ActionName}' Message='Transaction committed.' TransactionId='{TransactionId}'", ActionName, transaction!.TransactionId);
        }

        private async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            await transaction.RollbackToSavepointAsync(SavepointName, cancellationToken);
            
            _logger.LogInformation("Action='{ActionName}' Message='Transaction rollback to the savepoint.' TransactionId='{TransactionId}' Savepoint='{Savepoint}'", ActionName, transaction!.TransactionId, SavepointName);

            if (!_isNestedTransaction)
                await transaction.DisposeAsync();
        }
    }
}