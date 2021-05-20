using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.Data.Repositories.Custom.Impl
{
    internal class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Wallet> All => Query
            .Include(wallet => wallet.Currency)
            .Include(wallet => wallet.Transactions);
        
        public async Task<bool> ExistAsync(int walletId, int userId, CancellationToken cancellationToken = default)
        {
            Assert.IsGreaterThanZero(walletId, nameof(walletId));
            Assert.IsGreaterThanZero(userId, nameof(userId));
            cancellationToken.ThrowIfCancellationRequested();

            return await All.CountAsync(p => p.Id == walletId && p.UserId == userId, cancellationToken) != 0;
        }

        public Task<Wallet?> GetAsync(int walletId, int userId, CancellationToken cancellationToken)
        {
            Assert.IsGreaterThanZero(walletId, nameof(walletId));
            Assert.IsGreaterThanZero(userId, nameof(userId));
            cancellationToken.ThrowIfCancellationRequested();

            return All.SingleOrDefaultAsync(p => p.Id == walletId && p.UserId == userId, cancellationToken)!;
        }

        public Task<List<Wallet>> ListAsync(
            int userId,
            int top = default,
            int skip = default,
            CancellationToken cancellationToken = default)
        {
            Assert.IsGreaterThanZero(userId, nameof(userId));
            cancellationToken.ThrowIfCancellationRequested();

            return All.Where(p => p.UserId == userId)
                .Skip(skip)
                .Take(top)
                .ToListAsync(cancellationToken);
        }
    }
}