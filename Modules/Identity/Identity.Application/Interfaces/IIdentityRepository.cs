using Domain.Models;

namespace Application.Interfaces;

public interface IIdentityRepository
{
    void Add(Domain.Models.Identity identity);
    Task<Domain.Models.Identity?> GetByEmailAsync(string email);
    Task<Domain.Models.Identity?> GetByIdAsync(int identityId);

    Task SaveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshToken(int identityId);
    
    Task DeleteRefreshToken(RefreshToken refreshToken);
    Task SaveChangesAsync();
}