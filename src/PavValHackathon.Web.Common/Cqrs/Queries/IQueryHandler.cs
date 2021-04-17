using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}