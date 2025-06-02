using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Utils;

public static class JwtUtils
{
    public static bool IsTokenValid(string token, out JwtSecurityToken? securityToken, string issuer, string audience, string key)
    {
        if (!token.StartsWith("Bearer "))
        {
            securityToken = null;
            return false;
        }
        token = token.Replace("Bearer ", "");
        var validationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateLifetime = true
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            securityToken = (JwtSecurityToken)validatedToken;
            return true;
        }
        catch (Exception ex)
        {
            securityToken = null;
            return false;
        }
    }
}