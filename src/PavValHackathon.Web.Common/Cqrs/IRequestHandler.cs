using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs
{
    public interface IRequestHandler<in TRequest, TResult>
    {
        Task<Result<TResult>> HandleAsync(TRequest query, CancellationToken cancellationToken = default);
    }
}