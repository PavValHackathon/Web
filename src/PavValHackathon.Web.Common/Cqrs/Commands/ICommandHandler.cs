using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : class, ICommand
    {
        Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}