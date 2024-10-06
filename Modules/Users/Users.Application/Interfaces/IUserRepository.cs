namespace Users.Infrastructure.Persistence.Repositories;

public interface IUserRepository
{
    void Add(Domain.Models.User user);
}