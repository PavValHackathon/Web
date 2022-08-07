using System;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;

namespace PavValHackathon.Web.API.v1.Commands.Transactions.Handlers
{
    public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, int>
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IReadOnlyRepository<Currency> _currencyReadOnlyRepository;
        private readonly IMapperDefinition<CreateTransactionCommand, Transaction> _mapperDefinition;

        public CreateTransactionCommandHandler(
            IRepository<Transaction> transactionRepository,
            IReadOnlyRepository<Currency> currencyReadOnlyRepository,
            IMapperDefinition<CreateTransactionCommand, Transaction> mapperDefinition)
        {
            _transactionRepository =
                transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _currencyReadOnlyRepository = currencyReadOnlyRepository ??
                                          throw new ArgumentNullException(nameof(currencyReadOnlyRepository));
            _mapperDefinition = mapperDefinition ?? throw new ArgumentNullException(nameof(mapperDefinition));
        }

        public async Task<Result<int>> HandleAsync(CreateTransactionCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            if (!command.WalletId.HasValue)
                return this.InternalError();

            var currencyExist = await _currencyReadOnlyRepository.ExistAsync(command.CurrencyId, cancellationToken);

            if (!currencyExist)
                return this.NotFound("Currency not found.");

            var transaction = _mapperDefinition.Map(command);
            transaction = await _transactionRepository.CreateAsync(transaction, cancellationToken);
            
            return this.Ok(transaction.Id);
        }
    }
}