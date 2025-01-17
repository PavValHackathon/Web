﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Data.Repositories
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> 
        where TEntity : class, IDomainEntity
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicateExpression, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}