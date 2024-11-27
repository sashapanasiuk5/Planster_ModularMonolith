using User.Application.Interfaces;

namespace Users.Infrastructure.Persistence.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task SaveChangesAsync();
}