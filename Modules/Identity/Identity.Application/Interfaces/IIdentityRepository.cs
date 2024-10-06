using Domain.Models;

namespace Application.Interfaces;

public interface IIdentityRepository
{
    void Add(Domain.Models.Identity identity);
    Task<Domain.Models.Identity?> GetByEmailAsync(string email);
    Task SaveChangesAsync();
}