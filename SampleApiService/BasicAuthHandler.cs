using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SampleApiService
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly ILogger<BasicAuthHandler> _logger;

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,  // <-- inject logger factory
            UrlEncoder encoder)
            : base(options, loggerFactory, encoder)
        {
            _logger = loggerFactory.CreateLogger<BasicAuthHandler>();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string CorrelationId = Request.Headers["CorrelationId"].ToString();
            Serilog.Context.LogContext.PushProperty("CorrelationId", CorrelationId);

            _logger.LogInformation("Incoming request has CorrelationId in Header: {CorrelationId}",
                CorrelationId);

            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                var authHeaderVal = authHeader.Substring("Basic ".Length).Trim();
                var credentialBytes = Convert.FromBase64String(authHeaderVal);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                // TODO: Replace with your real validation
                if (username == "swift" && password == "swift@123!")
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
                }
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
        }
    }
}
