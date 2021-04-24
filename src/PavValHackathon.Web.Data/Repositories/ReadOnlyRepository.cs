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
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        protected ReadOnlyRepository(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public virtual Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            Assert.IsGreaterThanZero(id, nameof(id));
            cancellationToken.ThrowIfCancellationRequested();

            return _dbSet.FirstOrDefaultAsync(p => p.Id == id, cancellationToken)!;
        }

        public virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(predicateExpression, nameof(predicateExpression));
            cancellationToken.ThrowIfCancellationRequested();
            
            return _dbSet.FirstOrDefaultAsync(predicateExpression, cancellationToken)!;
        }

        public virtual Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return _dbSet.ToListAsync(cancellationToken);
        }

        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(predicateExpression, nameof(predicateExpression));
            cancellationToken.ThrowIfCancellationRequested();

            return _dbSet
                .Where(predicateExpression)
                .ToListAsync(cancellationToken);
        }
    }
}