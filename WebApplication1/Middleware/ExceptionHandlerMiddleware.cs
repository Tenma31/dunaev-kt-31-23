using System.Net;
using System.Text.Json;

namespace WebApplication1.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "Произошла необработанная ошибка");

                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";

                var responseModel = new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = exception.Message,
                    Errors = new List<string> { exception.InnerException?.Message ?? "Внутренняя ошибка" }
                };

                httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpResponse.WriteAsJsonAsync(responseModel);
            }
        }
    }

    public class ResponseModel<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public ResponseModel() { }

        public ResponseModel(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseModel(string message)
        {
            Succeeded = true;
            Message = message;
        }
    }
}