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
            Location = user.Location,
            Contacts = user.Contacts.Select(x => x.ToContactDto()).ToList(),
        };
    }


    public static ContactDto ToContactDto(this Users.Domain.Models.Contact contact)
    {
        return new ContactDto()
        {
            Type = contact.Type,
            Link = contact.Value
        };
    }
}