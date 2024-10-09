using Application.Interfaces;
using FluentResults;
using MediatR;

namespace Application.Commands.UpdatePermissions;

public class UpdatePermissionsCommandHandler: IRequestHandler<UpdatePermissionsCommand, Result<Unit>>
{
    private readonly ISessionStore _sessionStore;
    private readonly IIdentityRepository _identityRepository;
    private readonly IEncryptor _encryptor;

    public UpdatePermissionsCommandHandler(ISessionStore sessionStore, IIdentityRepository identityRepository, IEncryptor encryptor)
    {
        _sessionStore = sessionStore;
        _encryptor = encryptor;
        _identityRepository = identityRepository;
    }
    public async Task<Result<Unit>> Handle(UpdatePermissionsCommand request, CancellationToken cancellationToken)
    {
        var sessionId = _encryptor.Encrypt(request.IdentityId.ToString());
        var session = await _sessionStore.GetById(sessionId);
        if (session != null)
        {
            session.UpdatePermissions(request.Permissions);
            await _sessionStore.UpdateSessionAsync(session);
        }

        return Result.Ok();
    }
}