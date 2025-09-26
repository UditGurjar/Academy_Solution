using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace AcademyWeb.Authentication
{
    public class BasicAndJwtAuthHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAndJwtAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //if (!Request.Headers.ContainsKey("Authorization"))
            //    return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

            //var authHeader = Request.Headers["Authorization"].ToString();

            //if (authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            //{
            //    return HandleBasicAuth(authHeader.Substring("Basic ".Length).Trim());
            //}
            //else if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            //{
            //    return HandleJwtAuth(authHeader.Substring("Bearer ".Length).Trim());

            //}
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));

        }
    }           
}
