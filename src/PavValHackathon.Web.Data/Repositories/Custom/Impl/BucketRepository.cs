using System.Linq;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.Data.Repositories.Custom.Impl
{
    public class BucketRepository : Repository<Bucket>
    {
        public BucketRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Bucket> All => Query
            .Include(p => p.Currency)
            .Include(p => p.Transactions);
    }
}