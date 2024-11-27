using User.Application.Interfaces;

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

    public void Add(Domain.Models.User user)
    {
        _usersDbContext.Users.Add(user);
    }
}