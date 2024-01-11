using RestAPI.Exceptions;

namespace RestAPI.Middleware
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (ForbidException forbidException)
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync(forbidException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                httpContext.Response.StatusCode = 404;
                await httpContext.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
