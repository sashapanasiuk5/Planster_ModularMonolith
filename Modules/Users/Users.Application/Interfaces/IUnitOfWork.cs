namespace User.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task SaveChangesAsync();
}