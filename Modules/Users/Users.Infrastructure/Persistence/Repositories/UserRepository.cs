namespace Users.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UserRepository(UsersDbContext context)
    {
        _usersDbContext = context;
    }
    public void Add(Domain.Models.User user)
    {
        _usersDbContext.Users.Add(user);
    }
}