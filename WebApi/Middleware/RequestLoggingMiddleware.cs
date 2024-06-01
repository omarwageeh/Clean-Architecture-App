namespace WebApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            _logger.LogInformation("Received request: {Method} {Path}", context.Request.Method, context.Request.Path);

           var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                responseBody.Seek(0, SeekOrigin.Begin);
                var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation("Sent response: {StatusCode} {Content} From {Method} {Path}", context.Response.StatusCode, responseContent, context.Request.Method, context.Request.Path);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
