namespace User.Application.Interfaces;

public interface IUserRepository
{
    Task<Users.Domain.Models.User?> GetByIdAsync(int userId); 
    void Add(Users.Domain.Models.User user);
}