using System.Diagnostics;

namespace RestAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopwatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger) 
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(httpContext);

            _stopwatch.Stop();
            var elapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
            if (elapsedMiliseconds / 1000 > 4)
            {
                var message = $"Request [{httpContext.Request.Method}] at {httpContext.Request.Path} took {elapsedMiliseconds} ms";

                _logger.LogInformation(message);
            }
        }
    }
}
