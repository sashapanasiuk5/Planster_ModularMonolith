using System.Security.Claims;
using System.Text.Encodings.Web;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Contracts.ModulesInterfaces;

namespace Infrastructure.Utils;

public class SessionAuthSchemeHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IIdentityModule _identityModule;
    private readonly IEncryptor _encryptor;

    public SessionAuthSchemeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        IIdentityModule identityModule,
        ILoggerFactory logger,
        ISystemClock systemClock,
        IEncryptor encryptor,
        UrlEncoder encoder) : base(options, logger, encoder, systemClock)
    {
        _identityModule = identityModule;
        _encryptor = encryptor;
    }
    
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var sessionId = Context.Request.Cookies["SessionID"];
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return AuthenticateResult.Fail("No Session ID was specified.");
        }
            
        var session = await _identityModule.GetSessionAsync(sessionId);
        if (session != null)
        {
            var decrypted = _encryptor.Decrypt(session.Id);
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, session.IdentityId.ToString()) };
            foreach (var permission in session.Permissions)
            {
                var claim = new Claim($"project_{permission.ProjectId}", permission.Role.ToString());
                claims.Add(claim);
            }
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return AuthenticateResult.Success(ticket);

        }
        return AuthenticateResult.Fail("SessionID is incorrect or expired");
    }
}