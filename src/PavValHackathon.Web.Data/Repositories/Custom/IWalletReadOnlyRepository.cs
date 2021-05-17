using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.Data.Repositories.Custom
{
    public interface IWalletReadOnlyRepository : IReadOnlyRepository<Wallet>
    {
        Task<bool> ExistAsync(int walletId, int userId, CancellationToken cancellationToken = default);
        Task<Wallet?> GetAsync(int walletId, int userId, CancellationToken cancellationToken = default);
        Task<List<Wallet>> ListAsync(
            int userId,
            int top = default,
            int skip = default,
            CancellationToken cancellationToken = default);
    }
}