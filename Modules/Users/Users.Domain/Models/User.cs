namespace Users.Domain.Models;

public class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? ImageUrl { get; private set; }

    public User(string firstName, string lastName, string email, string? imageUrl)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ImageUrl = imageUrl;
    }
}