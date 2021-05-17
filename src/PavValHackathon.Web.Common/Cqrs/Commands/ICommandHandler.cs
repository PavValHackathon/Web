using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : class, ICommand<TResult>
    {
        Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}