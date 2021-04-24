using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Queries
{
    public interface IQueryExecutor
    {
        Task<Result<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : class, IQuery<TResult>;
    }
}