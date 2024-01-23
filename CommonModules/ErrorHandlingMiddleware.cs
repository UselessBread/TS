using Common.Exceptions;
using RestSharp;
using System.Text.Json;

namespace Common.Web
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Exception innermost = ex;
                while (innermost.InnerException != null)
                {
                    innermost = innermost.InnerException;
                }
                _logger.LogError(innermost, "Unhandled exception");

                ExceptionReturnMessage exceptionMessage = innermost is VerbalizedException verbalaizedException
                                   ? verbalaizedException.ToExceptionMessage()
                                   : new ExceptionReturnMessage(Constants.ErrorCodes.UnhandledError, innermost.Message);

                context.Response.ContentType = ContentType.Json;
                context.Response.StatusCode = exceptionMessage.GetHttpStatusCode();
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true};
                await context.Response.WriteAsync(JsonSerializer.Serialize(exceptionMessage, options));
            }
        }
    }
}
