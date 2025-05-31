using Domain.Models;
using Identity.Contracts.Dtos;
using Shared.Contracts.Dto.Teams.Member;

namespace Application.Interfaces;

using Identity = Domain.Models.Identity;
public interface ITokenProvider
{
    string CreateUserToken(int identityId);
    
    string CreateProjectToken(MemberPermissionDto? permissions);
    
    Task<string> CreateRefreshToken(string userToken, string? projectToken, int identityId);

    Task<string> RotateTokens(string userToken, string? projectToken, RefreshToken refreshToken);
    
    Task<string> RotateTokens(string projectToken, RefreshToken refreshToken);
}