namespace Domain.Models;

public class Identity
{
    public int Id { get; private set; }
    public Credentials Credentials { get; private set; }

    public Identity(int id, Credentials credentials)
    {
        Id = id;
        Credentials = credentials;
    }
}