using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.Data.Repositories.Custom
{
    public interface IWalletRepository : IWalletReadOnlyRepository, IRepository<Wallet>
    {
    }
}