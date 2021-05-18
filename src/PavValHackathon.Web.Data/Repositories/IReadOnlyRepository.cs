using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Data.Repositories
{
    public interface IReadOnlyRepository<TEntity>
        where TEntity : class, IDomainEntity
    {
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default); 
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        
        Task<bool> ExistAsync(int id, CancellationToken cancellationToken = default);
        
        Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default);
        
        Task<List<TEntity>> ListAsync(int top = default,
            int skip = default,
            CancellationToken cancellationToken = default);
        Task<List<TEntity>> ListAsync(
            Expression<Func<TEntity, bool>> predicateExpression,
            int top = default,
            int skip = default,
            CancellationToken cancellationToken = default);
    }
}