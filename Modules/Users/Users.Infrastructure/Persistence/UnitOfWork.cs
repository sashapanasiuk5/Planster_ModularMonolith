using User.Application.Interfaces;
using Users.Infrastructure.Persistence.Repositories;

namespace Users.Infrastructure.Persistence;

public class UnitOfWork: IUnitOfWork
{
    private readonly UsersDbContext _context;
    private IUserRepository? _userRepository;
    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_context);
            }

            return _userRepository;
        }
    }

    public UnitOfWork(UsersDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}