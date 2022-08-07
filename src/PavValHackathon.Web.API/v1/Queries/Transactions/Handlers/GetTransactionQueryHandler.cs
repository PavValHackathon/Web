using System;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;

namespace PavValHackathon.Web.API.v1.Queries.Transactions.Handlers
{
    public class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, TransactionDocument>
    {
        private readonly IMapperDefinition<Transaction, TransactionDocument> _mapperDefinition;
        private readonly IReadOnlyRepository<Transaction> _readOnlyRepository;

        public GetTransactionQueryHandler(
            IMapperDefinition<Transaction, TransactionDocument> mapperDefinition,
            IReadOnlyRepository<Transaction> readOnlyRepository)
        {
            _mapperDefinition = mapperDefinition ?? throw new ArgumentNullException(nameof(mapperDefinition));
            _readOnlyRepository = readOnlyRepository ?? throw new ArgumentNullException(nameof(readOnlyRepository));
        }

        public async Task<Result<TransactionDocument>> HandleAsync(GetTransactionQuery query, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            var transaction = await _readOnlyRepository.GetAsync(query.Id, cancellationToken);

            if (transaction is null)
                return this.NotFound("Transaction not found.");
            
            var transactionDocument = _mapperDefinition.Map(transaction);

            return this.Ok(transactionDocument);
        }
    }
}