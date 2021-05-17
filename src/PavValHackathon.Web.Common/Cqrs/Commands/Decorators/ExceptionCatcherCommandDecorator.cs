using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Decorators
{
    public class ExceptionCatcherCommandDecorator<TCommand, TResult> : CommandHandlerDecorator<TCommand, TResult>
        where TCommand : class, ICommand<TResult>
    {
        private readonly ILogger<ExceptionCatcherCommandDecorator<TCommand, TResult>> _logger;
        
        public ExceptionCatcherCommandDecorator(
            ILogger<ExceptionCatcherCommandDecorator<TCommand, TResult>> logger,
            ICommandHandler<TCommand, TResult> innerHandler) : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await InnerHandler.HandleAsync(command, cancellationToken);
            }
            catch (Exception e)
            {
                var commandName = typeof(TCommand).Name;
                var result = Result.Failed<TResult>((int) HttpStatusCode.InternalServerError, "Umm...");
                
                _logger.LogError(e, "Action='{CommandName}' TrackId='{TrackId}' Message='Command  handler throw an exception.'", commandName, result.TraceId);
                
                return result;
            }
        }
    }
}