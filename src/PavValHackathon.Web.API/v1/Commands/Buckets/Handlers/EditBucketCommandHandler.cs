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
    public class EditBucketCommandHandler : ICommandHandler<EditBucketCommand, Void>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<Bucket> _bucketRepository;
        private readonly IReadOnlyRepository<Currency> _currencyReadOnlyRepository;

        public EditBucketCommandHandler(
            IUserContext userContext, 
            IRepository<Bucket> bucketRepository,
            IReadOnlyRepository<Currency> currencyReadOnlyRepository)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _bucketRepository = bucketRepository ?? throw new ArgumentNullException(nameof(bucketRepository));
            _currencyReadOnlyRepository = currencyReadOnlyRepository ?? throw new ArgumentNullException(nameof(currencyReadOnlyRepository));
        }

        public async Task<Result<Void>> HandleAsync(EditBucketCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var bucketDomain = await _bucketRepository.GetAsync(p => p.Id == command.Id && p.UserId == userId, cancellationToken);

            if (bucketDomain is null)
                return this.NotFound("Bucket not found.");

            if (command.CurrencyId.HasValue &&
                command.CurrencyId != bucketDomain.CurrencyId)
            {
                if (await _currencyReadOnlyRepository.ExistAsync(command.CurrencyId.Value, cancellationToken) == false)
                    return this.NotFound("Currency not found.");

                bucketDomain.CurrencyId = command.CurrencyId.Value;
            }

            bucketDomain.PictureId = command.PictureId ?? bucketDomain.PictureId;
            bucketDomain.Title = command.Title ?? bucketDomain.Title;

            await _bucketRepository.UpdateAsync(bucketDomain, cancellationToken);
            
            return this.Ok();
        }
    }
}