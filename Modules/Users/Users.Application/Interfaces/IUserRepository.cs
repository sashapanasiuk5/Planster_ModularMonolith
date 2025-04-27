using Users.Domain.Enums;
using Users.Domain.Models;

namespace User.Application.Interfaces;

public interface IUserRepository
{
    Task<Users.Domain.Models.User?> GetByIdAsync(int userId);
    Task<Users.Domain.Models.User?> GetByIdWithDetailsAsync(int userId);
    Task<ProfilePhoto?> GetProfilePhotoAsync(int userId, PhotoResolution resolution);
    
    Task DeleteProfilePhotoAsync(int userId);
    void Add(Users.Domain.Models.User user);
}