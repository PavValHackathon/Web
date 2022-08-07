using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common.Cqrs.Queries;

namespace PavValHackathon.Web.API.v1.Queries.Transactions
{
    public class GetTransactionQuery : IQuery<TransactionDocument>
    {
        public GetTransactionQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}