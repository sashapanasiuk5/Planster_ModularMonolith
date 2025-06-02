using Application.Interfaces;
using Application.Mappers;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.Dto.Teams.Member;
using Shared.Contracts.ModulesInterfaces;
using Teams.Domain.Enums;

namespace Application.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommand, Result<LoginResultDto>>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITeamsModule _teamsModule;
    private readonly ITokenProvider _tokenProvider;
    private readonly IIdentityRepository _identityRepository;
    
    public LoginCommandHandler(IPasswordHasher passwordHasher, IIdentityRepository identityRepository, ITokenProvider tokenProvider, ITeamsModule teamsModule)
    {
        _passwordHasher = passwordHasher;
        _identityRepository = identityRepository;
        _teamsModule = teamsModule;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<LoginResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identity = await _identityRepository.GetByEmailAsync(request.Dto.Email);
        if (identity != null)
        {
            var isPasswordCorrect = _passwordHasher.Verify(request.Dto.Password, identity.Credentials.Password);
            if (isPasswordCorrect)
            {
                var userToken = _tokenProvider.CreateUserToken(identity.Id);
                string? projectToken = null;
                
                var permissions = await _teamsModule.GetMemberPermissionsAsync(identity.Id);
                if (permissions.Count > 0)
                {
                    var permission = permissions[0];
                    projectToken = _tokenProvider.CreateProjectToken(permission);
                }

                var refreshToken = await _tokenProvider.CreateRefreshToken(userToken, projectToken, identity.Id);
                return Result.Ok(new LoginResultDto(userToken, projectToken, refreshToken));
            }
            return Result.Fail("Invalid credentials");
        }
        return Result.Fail("User does not exist");
    }
}