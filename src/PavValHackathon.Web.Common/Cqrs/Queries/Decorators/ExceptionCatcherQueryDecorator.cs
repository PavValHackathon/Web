using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PavValHackathon.Web.Common.Cqrs.Queries.Decorators
{
    public class ExceptionCatcherQueryDecorator<TQuery, TResult> : QueryHandlerDecorator<TQuery, TResult> 
        where TQuery : class, IQuery<TResult>
    {
        private readonly ILogger<ExceptionCatcherQueryDecorator<TQuery, TResult>> _logger;
        
        public ExceptionCatcherQueryDecorator(
            IQueryHandler<TQuery, TResult> innerHandler, 
            ILogger<ExceptionCatcherQueryDecorator<TQuery, TResult>> logger) : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            try
            {
                return await InnerHandler.HandleAsync(query, cancellationToken);
            }
            catch (Exception e)
            {
                var queryName = typeof(TQuery).Name;
                var result = Result.Failed<TResult>((int) HttpStatusCode.InternalServerError, "Umm...");
                
                _logger.LogError(e, "Action='{QueryName}' TrackId='{TrackId}' Message='Query handler throw an exception.'", queryName, result.TraceId);
                
                return result;
            }
        }
    }
}