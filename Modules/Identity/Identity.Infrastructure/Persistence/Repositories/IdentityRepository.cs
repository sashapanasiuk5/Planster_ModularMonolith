using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class IdentityRepository: IIdentityRepository
{
    private readonly IdentityDbContext _context;

    public IdentityRepository(IdentityDbContext context)
    {
        _context = context;
    }
    public void Add(Domain.Models.Identity identity)
    {
        _context.Identities.Add(identity);
    }

    public async Task<Domain.Models.Identity?> GetByEmailAsync(string email)
    {
        return await _context.Identities.FirstOrDefaultAsync(x => x.Credentials.Email == email);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}