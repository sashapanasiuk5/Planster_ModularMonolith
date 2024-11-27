using FluentResults;

namespace Users.Domain.Models;

public class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Location { get; private set; }
    public string Email { get; private set; }
    public string? ImageUrl { get; private set; }

    private List<Contact> _contacts = new ();
    public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();
    public User(string firstName, string lastName, string email, string? imageUrl, string location)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Location = location;
        ImageUrl = imageUrl;
    }

    public Result AddContact(Contact contact)
    {
        if(_contacts.Exists(c => c.Type == contact.Type))
            return Result.Fail("Contact of same type is already added");
        _contacts.Add(contact);
        return Result.Ok();
    }
}