using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Data.Repositories
{
    public interface IReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default);
        
        Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default);
    }
}