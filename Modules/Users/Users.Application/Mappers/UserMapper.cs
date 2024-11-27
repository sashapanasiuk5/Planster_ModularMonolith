using Users.Contracts.Dto;

namespace User.Application.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this Users.Domain.Models.User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ImageUrl = user.ImageUrl,
        };
    }
}