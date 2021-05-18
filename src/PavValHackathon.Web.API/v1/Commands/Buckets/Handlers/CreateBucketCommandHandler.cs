using System;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;

namespace PavValHackathon.Web.API.v1.Commands.Buckets.Handlers
{
    public class CreateBucketCommandHandler : ICommandHandler<CreateBucketCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<Bucket> _bucketRepository;
        private readonly IReadOnlyRepository<Currency> _currencyRepository;
        private readonly IMapperDefinition<CreateBucketCommand, Bucket> _mapperDefinition;

        public CreateBucketCommandHandler(
            IUserContext userContext, 
            IRepository<Bucket> bucketRepository,
            IReadOnlyRepository<Currency> currencyRepository,
            IMapperDefinition<CreateBucketCommand, Bucket> mapperDefinition)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _bucketRepository = bucketRepository ?? throw new ArgumentNullException(nameof(bucketRepository));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
            _mapperDefinition = mapperDefinition ?? throw new ArgumentNullException(nameof(mapperDefinition));
        }

        public async Task<Result<int>> HandleAsync(CreateBucketCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            if (await _currencyRepository.ExistAsync(command.CurrencyId, cancellationToken) == false)
                return this.NotFound("Currency not found.");

            var userId = _userContext.UserId;
            
            var bucketDomain = _mapperDefinition.Map(command);
            bucketDomain.UserId = userId;
            bucketDomain = await _bucketRepository.CreateAsync(bucketDomain, cancellationToken); 
            
            return this.Ok(bucketDomain.Id);
        }
    }
}