namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : class, ICommand;
    }
}