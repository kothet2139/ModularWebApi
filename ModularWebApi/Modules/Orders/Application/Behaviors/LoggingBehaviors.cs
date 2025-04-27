using MediatR;

namespace ModularWebApi.Modules.Orders.Application.Behaviors
{
    public class LoggingBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviors<TRequest, TResponse>> _logger;

        public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);
            var response = await next();
            _logger.LogInformation("Handled {RequestName}", typeof (TRequest).Name);

            return response;
        }
    }
}
