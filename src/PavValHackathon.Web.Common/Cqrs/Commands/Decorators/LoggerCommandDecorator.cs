using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Decorators
{
    public class LoggerCommandDecorator<TCommand> : CommandHandlerDecorator<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ILogger<LoggerCommandDecorator<TCommand>> _logger;

        public LoggerCommandDecorator(
            ICommandHandler<TCommand> innerHandler,
            ILogger<LoggerCommandDecorator<TCommand>> logger) : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            if (!_logger.IsEnabled(LogLevel.Information))
                return await InnerHandler.HandleAsync(command, cancellationToken);

            var commandName = typeof(TCommand).Name;
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Action='{Command}' Message='Command handler started.'", commandName);
            
            try
            {
                return await InnerHandler.HandleAsync(command, cancellationToken);
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation("Action='{Command}' ElapsedMilliseconds='{ElapsedMilliseconds}' Message='Command handler executed.'", commandName, timer.ElapsedMilliseconds);
            }
        }
    }
}