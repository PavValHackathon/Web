using System;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;
using Void = PavValHackathon.Web.Common.Void;

namespace PavValHackathon.Web.API.v1.Commands.Buckets.Handlers
{
    public class DeleteBucketCommandHandler : ICommandHandler<DeleteBucketCommand, Void>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<Bucket> _repository;

        public DeleteBucketCommandHandler(IUserContext userContext, IRepository<Bucket> repository)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<Void>> HandleAsync(DeleteBucketCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var bucket = await _repository.GetAsync(p => p.Id == command.Id && p.UserId == userId, cancellationToken);

            if (bucket is null)
                return this.NotFound("Bucket not found.");
            
            await _repository.DeleteAsync(bucket, cancellationToken);
            
            return this.Ok();
        }
    }
}