using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PavValHackathon.Web.Common.Cqrs.Queries.Decorators
{
    public class LoggerQueryDecorator<TQuery, TResult> : QueryHandlerDecorator<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        private readonly ILogger<LoggerQueryDecorator<TQuery, TResult>> _logger;

        public LoggerQueryDecorator(
            IQueryHandler<TQuery, TResult> innerHandler, 
            ILogger<LoggerQueryDecorator<TQuery, TResult>> logger) : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            if (!_logger.IsEnabled(LogLevel.Information))
                return await InnerHandler.HandleAsync(query, cancellationToken);

            var queryName = typeof(TQuery).Name;
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Action='{QueryName}' Message='Query handler started.'", queryName);
            
            try
            {
                return await InnerHandler.HandleAsync(query, cancellationToken);
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation("Action='{Query}' ElapsedMilliseconds='{ElapsedMilliseconds}' Message='Command handler executed.'", queryName, timer.ElapsedMilliseconds);
            }
        }
    }
}