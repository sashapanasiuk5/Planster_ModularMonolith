namespace Domain.Models;

public class Credentials
{
    public string Email { get; }
    public string Password { get; }

    public Credentials(string email, string password)
    {
        Email = email;
        Password = password;
    }
}