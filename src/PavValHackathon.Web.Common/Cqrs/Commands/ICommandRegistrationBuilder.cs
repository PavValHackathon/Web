namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandRegistrationBuilder<TCommand, TResult>
        where TCommand : class, ICommand<TResult>
    {
        ICommandRegistrationBuilder<TCommand, TResult> RegisterDecorator<TDecorator>()
            where TDecorator : CommandHandlerDecorator<TCommand, TResult>;

        void InstancePerLifetimeScope();
    }
}