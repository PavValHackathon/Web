using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandExecutor
    {
        Task<Result> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand;
    }
}