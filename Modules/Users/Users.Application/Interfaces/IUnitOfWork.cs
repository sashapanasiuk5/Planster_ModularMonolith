namespace Users.Infrastructure.Persistence.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task SaveChangesAsync();
}