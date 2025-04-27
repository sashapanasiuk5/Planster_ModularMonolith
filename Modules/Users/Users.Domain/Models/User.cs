using FluentResults;

namespace Users.Domain.Models;

public class User
{
    public int Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Location { get; private set; }
    public string Email { get; private set; }

    private readonly List<ProfilePhoto> _photos = new();

    private readonly List<Contact> _contacts = new ();
    public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();
    public IReadOnlyCollection<ProfilePhoto> Photos => _photos.AsReadOnly();
    public User(string firstName, string lastName, string email, string location)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Location = location;
    }

    public Result AddContact(Contact contact)
    {
        if(_contacts.Exists(c => c.Type == contact.Type))
            return Result.Fail("Contact of same type is already added");
        _contacts.Add(contact);
        return Result.Ok();
    }

    public void AddPhoto(ProfilePhoto photo)
    {
        _photos.Add(photo);
    }
}