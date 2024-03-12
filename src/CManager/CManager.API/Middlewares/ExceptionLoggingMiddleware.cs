using Sentry;

namespace CManager.API.Middlewares
{
    public class ExceptionLoggingMiddleware : IMiddleware
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ExceptionLoggingMiddleware(ILogger<ExceptionLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                SentrySdk.CaptureException(ex);
                throw;
            }
        }
    }
}
