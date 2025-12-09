namespace SampleApiService
{
    public class RequestLoggingMiddleware
    {
        private const string CorrelationIdHeader = "CorrelationId";
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var requestBody = "";

            if (request.ContentLength > 0) //&& request.Body.CanSeek
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            // Read request headers
            var requestHeaders = request.Headers
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            _logger.LogDebug(
               "Request {Method} {Path} => RequestHeader={RequestHeader}, RequestBody={RequestBody}",
               request.Method, request.Path, requestHeaders, requestBody);

            await _next(context);

           
            
        }
    }
}
