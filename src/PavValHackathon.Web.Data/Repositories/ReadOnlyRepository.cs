using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Common;

namespace PavValHackathon.Web.Data.Repositories
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IDomainEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        protected IQueryable<TEntity> Query => _dbSet.AsQueryable();
        protected virtual IQueryable<TEntity> All => Query;

        public ReadOnlyRepository(DbContext dbContext)
        {
            Assert.IsNotNull(dbContext, nameof(dbContext));
            
            _dbSet = dbContext.Set<TEntity>();
        }
        
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) 
            => Query.CountAsync(predicate, cancellationToken);
        
        public Task<int> CountAsync(CancellationToken cancellationToken = default) 
            => Query.CountAsync(cancellationToken);

        public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(p => p.Id == id, cancellationToken) != 0;
        }

        public virtual Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            Assert.IsGreaterThanZero(id, nameof(id));
            cancellationToken.ThrowIfCancellationRequested();

            return All.FirstOrDefaultAsync(p => p.Id == id, cancellationToken)!;
        }

        public virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(predicateExpression, nameof(predicateExpression));
            cancellationToken.ThrowIfCancellationRequested();
            
            return All.FirstOrDefaultAsync(predicateExpression, cancellationToken)!;
        }

        public virtual Task<List<TEntity>> ListAsync(
            int top = default,
            int skip = default,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return All.ToListAsync(cancellationToken);
        }

        public virtual Task<List<TEntity>> ListAsync(
            Expression<Func<TEntity, bool>> predicateExpression,
            int top = default,
            int skip = default,
            CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(predicateExpression, nameof(predicateExpression));
            cancellationToken.ThrowIfCancellationRequested();

            return All
                .Where(predicateExpression)
                .ToListAsync(cancellationToken);
        }
    }
}