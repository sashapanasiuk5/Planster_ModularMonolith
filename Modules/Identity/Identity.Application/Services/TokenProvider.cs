using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Domain.Models;
using Identity.Contracts.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.Constants;
using Shared.Contracts.Dto.Teams.Member;

namespace Application.Services;

using Identity = Domain.Models.Identity;

public class TokenProvider: ITokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityRepository _identityRepository;

    public TokenProvider(IConfiguration configuration, IIdentityRepository identityRepository)
    {
        _configuration = configuration;
        _identityRepository = identityRepository;
    }
    public string CreateUserToken(int identityId)
    {
        string secretKey = _configuration["Encryption:Key"];
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, identityId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:TokenLifetime")),
            SigningCredentials = credentials,
            Issuer = _configuration["Authentication:Issuer"],
            Audience = _configuration["Authentication:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokenDescriptor);
    }

    public string CreateProjectToken(MemberPermissionDto? permissions)
    {
        string secretKey = _configuration["Encryption:Key"];
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(TokenConstants.PROJECT_ID, permissions.ProjectId.ToString()),
                new Claim(TokenConstants.ROLE, ((int)permissions.Role).ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:TokenLifetime")),
            SigningCredentials = credentials,
            Issuer = _configuration["Authentication:Issuer"],
            Audience = _configuration["Authentication:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokenDescriptor);
    }

    public async Task<string> CreateRefreshToken(string userToken, string? projectToken, int identityId)
    {
        var randomNumber = new byte[TokenConstants.REFRESH_TOKEN_LENGTH];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        var tokenString = Convert.ToBase64String(randomNumber);
        var expiryDate = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:RefreshTokenLifetime"));
        var refreshToken = new RefreshToken(identityId,
            userToken,
            projectToken,
            tokenString,
            expiryDate);
        await _identityRepository.SaveRefreshToken(refreshToken);
        return tokenString;
    }

    public async Task<string> RotateTokens(string userToken, string? projectToken, RefreshToken refreshToken)
    {
        var randomNumber = new byte[TokenConstants.REFRESH_TOKEN_LENGTH];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        var tokenString = Convert.ToBase64String(randomNumber);
        
        refreshToken.AccessToken = userToken;
        refreshToken.ProjectToken = projectToken;
        refreshToken.Token = tokenString;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:RefreshTokenLifetime"));
        await _identityRepository.SaveChangesAsync();
        return tokenString;
    }
    
    public async Task<string> RotateTokens(string projectToken, RefreshToken refreshToken)
    {
        var randomNumber = new byte[TokenConstants.REFRESH_TOKEN_LENGTH];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        var tokenString = Convert.ToBase64String(randomNumber);
        
        refreshToken.ProjectToken = projectToken;
        refreshToken.Token = tokenString;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:RefreshTokenLifetime"));
        await _identityRepository.SaveChangesAsync();
        return tokenString;
    }
}