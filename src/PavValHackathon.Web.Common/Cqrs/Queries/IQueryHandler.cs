namespace PavValHackathon.Web.Common.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
    }
}