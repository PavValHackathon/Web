using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common.Cqrs.Queries;

namespace PavValHackathon.Web.API.v1.Queries.Wallets
{
    public class GetWalletQuery : IQuery<WalletDocument>
    {
        public GetWalletQuery(int id)
        {
            Id = id;
        }
        
        public int Id { get; }
    }
}