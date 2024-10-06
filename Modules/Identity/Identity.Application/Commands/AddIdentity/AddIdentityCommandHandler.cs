using Application.Handlers;
using Application.Interfaces;
using Domain.Models;
using FluentResults;
using Infrastructure.Dto;
using MediatR;

namespace Application.Commands.AddIdentity;

public class AddIdentityCommandHandler: IRequestHandler<AddIdentityCommand, Result<SessionDto>>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly IPasswordHasher _passwordHasher;
    
    public AddIdentityCommandHandler(IIdentityRepository identityRepository, IPasswordHasher hasher)
    {
        _identityRepository = identityRepository;
        _passwordHasher = hasher;
    }
    
    public async Task<Result<SessionDto>> Handle(AddIdentityCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Hash(request.dto.Password);
        var creds = new Credentials(request.dto.Email, hashedPassword);
        var identity = new Domain.Models.Identity(creds);
        _identityRepository.Add(identity);
        await _identityRepository.SaveChangesAsync();
        return Result.Ok(new SessionDto(){SessionId = "sdfsdfsdfsdfqwqweqwedsccvwe"});
    }
}