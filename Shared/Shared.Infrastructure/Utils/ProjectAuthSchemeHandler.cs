using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.ModulesInterfaces;

namespace Infrastructure.Utils;

public class ProjectAuthSchemeHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public ProjectAuthSchemeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        ISystemClock systemClock,
        IConfiguration configuration,
        UrlEncoder encoder) : base(options, logger, encoder, systemClock)
    {
        _configuration = configuration;
    }
    
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string userToken = null;
        if (Request.Headers.Authorization.Count > 0)
        {
            userToken = Request.Headers.Authorization[0];
        }
        string projectToken = null;
        if (Request.Headers["Authorization-Project"].Count > 0)
        {
            projectToken = Request.Headers["Authorization-Project"][0];
        }
        
        if (projectToken.IsNullOrEmpty() || !JwtUtils.IsTokenValid(projectToken,
                out JwtSecurityToken projectSecurityToken,
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                _configuration["Encryption:Key"]))
        {
            return Task.FromResult(AuthenticateResult.Fail("Project token is incorrect or expired"));
        }

        if (userToken.IsNullOrEmpty() || !JwtUtils.IsTokenValid(userToken,
                out JwtSecurityToken userSecurityToken,
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                _configuration["Encryption:Key"]))
        {
            return Task.FromResult(AuthenticateResult.Fail("User token is incorrect or expired"));
        }
        
        var claims = userSecurityToken!.Claims;
        claims = claims.Concat(projectSecurityToken!.Claims);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}