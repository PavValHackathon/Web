namespace PavValHackathon.Web.Common.Cqrs.Queries
{
    public interface IQueryRegistrationBuilder<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {   
        IQueryRegistrationBuilder<TQuery, TResult> RegisterDecorator<TDecorator>()
            where TDecorator : QueryHandlerDecorator<TQuery, TResult>;

        void InstancePerLifetimeScope();
    }
}