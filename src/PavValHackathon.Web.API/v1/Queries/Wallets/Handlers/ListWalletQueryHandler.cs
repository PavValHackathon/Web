using System;
using System.Linq;
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
    public class ListWalletQueryHandler : IQueryHandler<ListWalletQuery, PaginationCollection<WalletDocument>>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletReadOnlyRepository _readOnlyRepository;
        private readonly IMapperDefinition<Wallet, WalletDocument> _mapper;

        public ListWalletQueryHandler(
            IUserContext userContext, 
            IWalletReadOnlyRepository readOnlyRepository,
            IMapperDefinition<Wallet, WalletDocument> mapper)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _readOnlyRepository = readOnlyRepository ?? throw new ArgumentNullException(nameof(readOnlyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<PaginationCollection<WalletDocument>>> HandleAsync(ListWalletQuery query, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var wallets = await _readOnlyRepository.ListAsync(userId, top: query.Top, skip: query.Skip, cancellationToken);

            var totalCount = await _readOnlyRepository.CountAsync(p => p.UserId == userId, cancellationToken);
            var walletDocuments = wallets.Select(_mapper.Map).ToList();

            var paginationCollection = new PaginationCollection<WalletDocument>
            {
                Items = walletDocuments,
                Count = totalCount
            };
            
            return Result.Ok(paginationCollection); 
        }
    }
}