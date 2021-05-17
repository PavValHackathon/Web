using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandExecutor
    {
        Task<Result<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand<TResult>;
    }
}