namespace PavValHackathon.Web.Common.Cqrs.Queries
{
    public interface IQueryHandlerResolver
    {
        IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>() 
            where TQuery : class, IQuery<TResult>;
    }
}