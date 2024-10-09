using Application.Interfaces;
using Application.Mappers;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;

namespace Application.Commands.AddIdentity;

public class AddIdentityCommandHandler: IRequestHandler<AddIdentityCommand, Result<SessionDto>>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly IEncryptor _encryptor;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISessionStore _sessionStore;
    
    public AddIdentityCommandHandler(IIdentityRepository identityRepository, IPasswordHasher hasher, ISessionStore sessionStore, IEncryptor encryptor)
    {
        _identityRepository = identityRepository;
        _passwordHasher = hasher;
        _sessionStore = sessionStore;
        _encryptor = encryptor;
    }
    
    public async Task<Result<SessionDto>> Handle(AddIdentityCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Hash(request.dto.Password);
        var creds = new Credentials(request.dto.Email, hashedPassword);
        var session = Session.Create(request.UserId, new List<MemberPermissionDto>(), _encryptor);
        await _sessionStore.StartSessionAsync(session);
        
        var identity = new Domain.Models.Identity(creds, request.UserId);
        _identityRepository.Add(identity);
        await _identityRepository.SaveChangesAsync();
        
        return Result.Ok(new SessionDto()
        {
            Id = session.Id.ToString(),
            IdentityId = identity.Id,
            Permissions = new List<MemberPermissionDto>()
        });     
    }
}