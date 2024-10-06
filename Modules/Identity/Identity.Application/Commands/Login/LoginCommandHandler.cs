using Application.Interfaces;
using FluentResults;
using Infrastructure.Dto;
using MediatR;

namespace Application.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommand, Result<SessionDto>>
{
    private readonly ISessionStore _sessionStore;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IIdentityRepository _identityRepository;

    public LoginCommandHandler(ISessionStore sessionStore, IPasswordHasher passwordHasher, IIdentityRepository identityRepository)
    {
        _sessionStore = sessionStore;
        _passwordHasher = passwordHasher;
        _identityRepository = identityRepository;
    }
    public async Task<Result<SessionDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identity = await _identityRepository.GetByEmailAsync(request.Dto.Email);
        if (identity != null)
        {
            var isPasswordCorrect = _passwordHasher.Verify(request.Dto.Password, identity.Credentials.Password);
            if (isPasswordCorrect)
            {
                var session = await _sessionStore.StartSessionAsync(identity.Id);
                return Result.Ok(session);     
            }
            return Result.Fail("Invalid credentials");
        }
        return Result.Fail("User does not exist");
    }
}