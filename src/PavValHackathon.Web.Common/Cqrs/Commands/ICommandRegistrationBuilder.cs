namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public interface ICommandRegistrationBuilder<TCommand>
        where TCommand : class, ICommand
    {
        ICommandRegistrationBuilder<TCommand> RegisterDecorator<TDecorator>()
            where TDecorator : CommandHandlerDecorator<TCommand>;

        void InstancePerLifetimeScope();
    }
}