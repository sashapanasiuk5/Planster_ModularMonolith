using Application.Interfaces;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Shared.Contracts.ModulesInterfaces;

namespace Application.Commands.RefreshToken;

public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, Result<LoginResultDto>>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly ITeamsModule _teamsModule;
    private readonly ITokenProvider _tokenProvider;

    public RefreshTokenCommandHandler(IIdentityRepository identityRepository, ITeamsModule teamsModule, ITokenProvider tokenProvider)
    {
        _identityRepository = identityRepository;
        _teamsModule = teamsModule;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<LoginResultDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityRepository.GetRefreshToken(request.identityId);
        if (token == null)
        {
            return Result.Fail("Refresh token not found");
        }
        if (token.Token != request.refreshToken || token.IsExpired)
        {
            await _identityRepository.DeleteRefreshToken(token);
            await _identityRepository.SaveChangesAsync();
            return Result.Fail("Refresh token expired");
        }

        string userToken = _tokenProvider.CreateUserToken(request.identityId);
        string? projectToken = null;
        if (request.projectId != null)
        {
            var permissions = await _teamsModule.GetMemberPermissionsByProjectAsync(request.identityId, request.projectId.Value);
            projectToken = _tokenProvider.CreateProjectToken(permissions);
        }

        var refreshToken = await _tokenProvider.RotateTokens(userToken, projectToken, token);
        
        return Result.Ok(new LoginResultDto(userToken, projectToken, refreshToken));
    }
}