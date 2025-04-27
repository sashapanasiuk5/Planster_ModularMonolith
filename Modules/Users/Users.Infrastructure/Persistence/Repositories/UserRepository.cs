using Microsoft.EntityFrameworkCore;
using User.Application.Interfaces;
using Users.Domain.Enums;
using Users.Domain.Models;

namespace Users.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UserRepository(UsersDbContext context)
    {
        _usersDbContext = context;
    }

    public async Task<Domain.Models.User?> GetByIdAsync(int userId)
    {
        return await _usersDbContext.Users.FindAsync(userId);
    }

    public async Task<Domain.Models.User?> GetByIdWithDetailsAsync(int userId)
    {
        return await _usersDbContext.Users
            .Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public Task<ProfilePhoto?> GetProfilePhotoAsync(int userId, PhotoResolution resolution)
    {
        return _usersDbContext.Photos
            .Where(x => x.UserId == userId)
            .Where(x => x.Resolution == resolution)
            .FirstOrDefaultAsync();
    }

    public Task DeleteProfilePhotoAsync(int userId)
    {
        return _usersDbContext.Photos
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync();
    }

    public void Add(Domain.Models.User user)
    {
        _usersDbContext.Users.Add(user);
    }
}