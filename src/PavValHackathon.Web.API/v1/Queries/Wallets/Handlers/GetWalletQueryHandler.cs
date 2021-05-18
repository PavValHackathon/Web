using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories.Custom;

namespace PavValHackathon.Web.API.v1.Queries.Wallets.Handlers
{
    public class GetWalletQueryHandler : IQueryHandler<GetWalletQuery, WalletDocument>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletReadOnlyRepository _readOnlyRepository;
        private readonly IMapperDefinition<Wallet, WalletDocument> _mapper;

        public GetWalletQueryHandler(
            IUserContext userContext,
            IWalletReadOnlyRepository readOnlyRepository,
            IMapperDefinition<Wallet, WalletDocument> mapper)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _readOnlyRepository = readOnlyRepository ?? throw new ArgumentNullException(nameof(readOnlyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<WalletDocument>> HandleAsync(
            GetWalletQuery query, 
            CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var wallet = await _readOnlyRepository.GetAsync(query.Id, userId, cancellationToken);

            return wallet is null 
                ? Result.Failed<WalletDocument>((int) HttpStatusCode.NotFound, "Wallet not found.") 
                : Result.Ok(_mapper.Map(wallet));
        }
    }
}