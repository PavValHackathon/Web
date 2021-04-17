using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Decorators
{
    public class ExceptionCatcherCommandDecorator<TCommand> : CommandHandlerDecorator<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ILogger<ExceptionCatcherCommandDecorator<TCommand>> _logger;
        
        public ExceptionCatcherCommandDecorator(
            ILogger<ExceptionCatcherCommandDecorator<TCommand>> logger,
            ICommandHandler<TCommand> innerHandler) : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await InnerHandler.HandleAsync(command, cancellationToken);
            }
            catch (Exception e)
            {
                var commandName = typeof(TCommand).Name;
                var result = await Result.FailedAsync((int) HttpStatusCode.InternalServerError, "Umm...");
                
                _logger.LogError(e, "Action='{CommandName}' TrackId='{TrackId}' Message='Command  handler throw an exception.'", commandName, result.TraceId);
                
                return result;
            }
        }
    }
}