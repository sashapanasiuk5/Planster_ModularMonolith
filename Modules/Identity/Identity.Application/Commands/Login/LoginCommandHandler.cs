using Application.Interfaces;
using Application.Mappers;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Shared.Contracts.ModulesInterfaces;

namespace Application.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommand, Result<SessionDto>>
{
    private readonly ISessionStore _sessionStore;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITeamsModule _teamsModule;
    private readonly IEncryptor _encryptor;
    private readonly IIdentityRepository _identityRepository;

    public LoginCommandHandler(ISessionStore sessionStore, IPasswordHasher passwordHasher, IIdentityRepository identityRepository, ITeamsModule teamsModule, IEncryptor encryptor)
    {
        _sessionStore = sessionStore;
        _passwordHasher = passwordHasher;
        _identityRepository = identityRepository;
        _teamsModule = teamsModule;
        _encryptor = encryptor;
    }
    public async Task<Result<SessionDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identity = await _identityRepository.GetByEmailAsync(request.Dto.Email);
        if (identity != null)
        {
            var isPasswordCorrect = _passwordHasher.Verify(request.Dto.Password, identity.Credentials.Password);
            if (isPasswordCorrect)
            {
                var permissions = await _teamsModule.GetMemberPermissionsAsync(identity.Id);
                var session = Session.Create(identity.Id, permissions, _encryptor);
                await _sessionStore.StartSessionAsync(session);
                return Result.Ok(session.ToDto());     
            }
            return Result.Fail("Invalid credentials");
        }
        return Result.Fail("User does not exist");
    }
}