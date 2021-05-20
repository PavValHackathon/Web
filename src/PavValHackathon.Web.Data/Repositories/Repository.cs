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
        
        public virtual Task DeleteManyAsync(HashSet<int> ids, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(ids, nameof(ids));
            cancellationToken.ThrowIfCancellationRequested();

            var listEntityQuery = _dbSet.Where(p => ids.Contains(p.Id));
            _dbSet.RemoveRange(listEntityQuery);
            
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Assert.IsNotNull(entities, nameof(entities));
            cancellationToken.ThrowIfCancellationRequested();
            
            // ReSharper disable once PossibleMultipleEnumeration
            _dbSet.RemoveRange(entities);

            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(predicateExpression, nameof(predicateExpression));
            cancellationToken.ThrowIfCancellationRequested();
            
            var listEntityQuery = _dbSet.Where(predicateExpression);
            _dbSet.RemoveRange(listEntityQuery);
            
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(entity, nameof(entity));
            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entityEntry.Entity;
        }

        public virtual Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Assert.IsNotNull(entities, nameof(entities));
            cancellationToken.ThrowIfCancellationRequested();
            
            // ReSharper disable once PossibleMultipleEnumeration
            _dbSet.UpdateRange(entities);
            
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}