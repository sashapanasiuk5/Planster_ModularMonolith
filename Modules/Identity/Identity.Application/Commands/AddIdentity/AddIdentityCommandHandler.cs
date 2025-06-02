using Application.Interfaces;
using Application.Mappers;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;
using Shared.Contracts.ModulesInterfaces;

namespace Application.Commands.AddIdentity;

public class AddIdentityCommandHandler: IRequestHandler<AddIdentityCommand, Result<LoginResultDto>>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly ITeamsModule _teamsModule;
    
    public AddIdentityCommandHandler(IIdentityRepository identityRepository, IPasswordHasher hasher, ITokenProvider tokenProvider, ITeamsModule teamsModule)
    {
        _identityRepository = identityRepository;
        _passwordHasher = hasher;
        _tokenProvider = tokenProvider;
        _teamsModule = teamsModule;
    }
    
    public async Task<Result<LoginResultDto>> Handle(AddIdentityCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Hash(request.dto.Password);
        var creds = new Credentials(request.dto.Email, hashedPassword);
        var identity = new Domain.Models.Identity(creds, request.UserId);
        
        var userToken = _tokenProvider.CreateUserToken(identity.Id);
        string? projectToken = null;
                
        var permissions = await _teamsModule.GetMemberPermissionsAsync(identity.Id);
        if (permissions.Count > 0)
        {
            var permission = permissions[0];
            projectToken = _tokenProvider.CreateProjectToken(permission);
        }
        _identityRepository.Add(identity);
        var refreshToken = await _tokenProvider.CreateRefreshToken(userToken, projectToken, identity.Id);
        
        

        await _identityRepository.SaveChangesAsync();
        return Result.Ok(new LoginResultDto(userToken, projectToken, refreshToken));
    }
}