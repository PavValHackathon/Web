using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Common;

namespace PavValHackathon.Web.Data.Repositories
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IDomainEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        
        public Repository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(entity, nameof(entity));
            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return entityEntry.Entity;
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Assert.IsGreaterThanZero(id, nameof(id));
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _dbSet.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (entity is not null)
                _dbSet.Remove(entity);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(entity, nameof(entity));
            cancellationToken.ThrowIfCancellationRequested();

            _dbSet.Remove(entity);
            
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(predicateExpression, nameof(predicateExpression));
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _dbSet.FirstOrDefaultAsync(predicateExpression, cancellationToken);

            if (entity is not null)
                _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(entity, nameof(entity));
            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entityEntry.Entity;
        }
    }
}