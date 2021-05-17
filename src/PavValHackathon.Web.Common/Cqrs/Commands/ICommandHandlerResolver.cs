namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<TCommand, TResult> Resolve<TCommand, TResult>()
            where TCommand : class, ICommand<TResult>;
    }
}