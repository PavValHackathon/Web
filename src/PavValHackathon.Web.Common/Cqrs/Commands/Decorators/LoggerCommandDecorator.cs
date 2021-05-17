using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Decorators
{
    public class LoggerCommandDecorator<TCommand, TResult> : CommandHandlerDecorator<TCommand, TResult>
        where TCommand : class, ICommand<TResult>
    {
        private readonly ILogger<LoggerCommandDecorator<TCommand, TResult>> _logger;

        public LoggerCommandDecorator(
            ICommandHandler<TCommand, TResult> innerHandler,
            ILogger<LoggerCommandDecorator<TCommand, TResult>> logger) : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken)
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