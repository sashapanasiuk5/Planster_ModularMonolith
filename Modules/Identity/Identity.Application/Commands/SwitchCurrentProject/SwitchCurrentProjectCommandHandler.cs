using Application.Interfaces;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Shared.Contracts.ModulesInterfaces;

namespace Application.Commands.SwitchCurrentProject;

public class SwitchCurrentProjectCommandHandler: IRequestHandler<SwitchCurrentProjectCommand, Result<SwitchProjectDto>>
{
    private readonly ITeamsModule _teamsModule;
    private readonly ITokenProvider _tokenProvider;
    private readonly IIdentityRepository _identityRepository;

    public SwitchCurrentProjectCommandHandler(ITeamsModule teamsModule, ITokenProvider tokenProvider, IIdentityRepository identityRepository)
    {
        _teamsModule = teamsModule;
        _tokenProvider = tokenProvider;
        _identityRepository = identityRepository;
    }
    public async Task<Result<SwitchProjectDto>> Handle(SwitchCurrentProjectCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityRepository.GetRefreshToken(request.identityId);
        if (token == null)
        {
            return Result.Fail("Refresh token not found");
        }

        var permissions = await _teamsModule.GetMemberPermissionsByProjectAsync(request.identityId, request.projectId);
        if (permissions == null)
        {
            return Result.Fail("No permissions for this project");
        }

        var projectToken = _tokenProvider.CreateProjectToken(permissions);
        var refreshToken = await _tokenProvider.RotateTokens(projectToken, token);
        return Result.Ok(new SwitchProjectDto() { ProjectToken = projectToken, RefreshToken = refreshToken });
    }
}