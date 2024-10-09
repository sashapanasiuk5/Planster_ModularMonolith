using Application.Interfaces;
using Shared.Contracts.Dto.Teams.Member;

namespace Domain.Models;

public class Session
{
    public static readonly TimeSpan SessionDuration = TimeSpan.FromHours(1);
    public string Id { get; private set; }
    public int IdentityId { get; private set; }
    public DateTime ExpirationDateTime { get; private set; }
    private List<MemberPermissionDto> _permissions;
    public IReadOnlyCollection<MemberPermissionDto> Permissions => _permissions.AsReadOnly();

    public Session(){
    }
    public Session(string id, int identityId, DateTime expirationDateTime, List<MemberPermissionDto> permissions)
    {
        Id = id;
        IdentityId = identityId;
        ExpirationDateTime = expirationDateTime;
        _permissions = permissions;
    }
    public static Session Create(int identityId, List<MemberPermissionDto> permissions, IEncryptor encryptor)
    {
        return new Session(encryptor.Encrypt(identityId.ToString()), identityId, DateTime.UtcNow.Add(SessionDuration), permissions);
    }

    public void UpdatePermissions(List<MemberPermissionDto> permissions)
    {
        _permissions = permissions;
    }
}