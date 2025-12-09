namespace OcelotGatewayService
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeader = "CorrelationId";
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if incoming request has correlation id
            var correlationId = context.Request.Headers.ContainsKey(CorrelationIdHeader)
                ? context.Request.Headers[CorrelationIdHeader].ToString()
                : Guid.NewGuid().ToString();

            // Store it in HttpContext for later usage
            context.Items[CorrelationIdHeader] = correlationId;

            // Add to response header
            context.Response.OnStarting(() =>
            {
                context.Response.Headers[CorrelationIdHeader] = correlationId;
                return Task.CompletedTask;
            });

            // Add to logging scope
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                [CorrelationIdHeader] = correlationId
            }))
            {
                await _next(context);
            }
        }
    }

}
