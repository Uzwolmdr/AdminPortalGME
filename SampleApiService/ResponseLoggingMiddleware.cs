namespace SampleApiService
{
    public class ResponseLoggingMiddleware
    {
        private const string CorrelationIdHeader = "CorrelationId";
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            var requestHeaders = context.Request.Headers
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            // Get correlationId from request
            string correlationId = requestHeaders.TryGetValue(CorrelationIdHeader, out var _correlationId)
                ? _correlationId
                : Guid.NewGuid().ToString(); // fallback

            // Replace response stream
            var originalBody = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Continue pipeline
            await _next(context);

            // Set correlationId in response header
            context.Response.Headers[CorrelationIdHeader] = correlationId;

            // Read response
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogDebug(
                "Response => {StatusCode}, ResponseHeaders={ResponseHeaders}, ResponseBody={ResponseBody}",
                context.Response.StatusCode, context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                responseText);

            // Copy back to original body
            await responseBody.CopyToAsync(originalBody);
        }
    }
}
