using System.Linq;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.Data.Repositories.Custom.Impl
{
    public class TransactionRepository : Repository<Transaction>
    {
        public TransactionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Transaction> All => Query.Include(p => p.Currency);
    }
}