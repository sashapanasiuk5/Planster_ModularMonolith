using Application.Interfaces;
using Domain.Models;
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

    public async Task<Domain.Models.Identity?> GetByIdAsync(int identityId)
    {
        return await _context.Identities.FindAsync(identityId);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        var refreshTokenExists = await _context.RefreshTokens.AnyAsync(e => e.IdentityId == refreshToken.IdentityId);
        if (refreshTokenExists)
        {
            _context.RefreshTokens.Update(refreshToken);
        }
        else
        {
            _context.RefreshTokens.Add(refreshToken);
        }
        await _context.SaveChangesAsync();
    }

    public Task<RefreshToken?> GetRefreshToken(int identityId)
    {
        return _context.RefreshTokens.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }

    public Task DeleteRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}