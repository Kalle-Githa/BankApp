using System.Net;
using System.Text.Json;

namespace BankApp.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;

            switch (exception)
            {
                case ArgumentException:
                    status = HttpStatusCode.BadRequest; // 400
                    break;

                case InvalidOperationException:
                    status = HttpStatusCode.BadRequest; // 400
                    break;

                case UnauthorizedAccessException:
                    status = HttpStatusCode.Forbidden; // 403
                    break;

                case KeyNotFoundException:
                    status = HttpStatusCode.NotFound; // 404
                    break;

                default:
                    status = HttpStatusCode.InternalServerError; // 500
                    message = "Ett oväntat fel inträffade.";
                    break;
            }

            var response = new
            {
                status = (int)status,
                error = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

