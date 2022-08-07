using System;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;

namespace PavValHackathon.Web.API.v1.Commands.Transactions.Decorators
{
    public class UpdateBucketTransactionCommandDecorator<TCommand, TResult> : CommandHandlerDecorator<TCommand, TResult>
        where TCommand : TransactionCommand<TResult>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<Bucket> _bucketRepository;

        public UpdateBucketTransactionCommandDecorator(
            ICommandHandler<TCommand, TResult> innerHandler,
            IUserContext userContext, 
            IRepository<Bucket> bucketRepository) : base(innerHandler)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _bucketRepository = bucketRepository ?? throw new ArgumentNullException(nameof(bucketRepository));
        }

        public override async Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var bucket = await _bucketRepository.GetAsync(p => p.Id == command.BucketId && p.UserId == userId, cancellationToken);

            if (bucket is null)
                return this.NotFound("Bucket not found.");
            
            var innerResult = await InnerHandler.HandleAsync(command, cancellationToken);

            if (innerResult.IsFailed)
                return innerResult;

            bucket.Amount += command.Value;
            await _bucketRepository.UpdateAsync(bucket, cancellationToken);
            
            return innerResult;
        }
    }
}