using System.Net;
using System.Text.Json;

namespace Davaleba.Helpers
{
    public class MiddlewareErrorHandler
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _errorHandler;

        public MiddlewareErrorHandler(ILogger<MiddlewareErrorHandler> logger, RequestDelegate errorHandler)
        {
            _logger = logger;
            _errorHandler = errorHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _errorHandler(context);
            }
            catch (Exception Err)
            {
                var resp = context.Response;
                resp.ContentType = "application/json";

                switch (Err)
                {

                    case AppException e:
                        resp.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        resp.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        _logger.LogError(Err, Err.Message);
                        resp.StatusCode = (int)(HttpStatusCode.InternalServerError);
                        break;
                }

                var outresult = JsonSerializer.Serialize(new { Message = Err?.Message });
                await resp.WriteAsync(outresult);
            }

        }
    }
}
