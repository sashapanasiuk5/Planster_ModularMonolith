using System.Security.Claims;
using System.Text.Encodings.Web;
using Infrastructure.ModulesInterfaces;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Shared.Infrastructure.Middlewares;

public class SessionAuthSchemeHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IIdentityModule _identityModule;

    public SessionAuthSchemeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        IIdentityModule identityModule,
        ILoggerFactory logger,
        ISystemClock systemClock,
        UrlEncoder encoder) : base(options, logger, encoder, systemClock)
    {
        _identityModule = identityModule;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sessionId = context.Request.Cookies["SessionID"];
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
            
        var userId = await _identityModule.GetIdentityAsync(sessionId);
        if (userId != null)
        {
            await next(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var sessionId = Context.Request.Cookies["SessionID"];
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return AuthenticateResult.Fail("No Session ID was specified.");
        }
            
        var userId = await _identityModule.GetIdentityAsync(sessionId);
        if (userId != null)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, userId.Value.ToString()) };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return AuthenticateResult.Success(ticket);

        }
        return AuthenticateResult.Fail("SessionID is incorrect or expired");
    }
}