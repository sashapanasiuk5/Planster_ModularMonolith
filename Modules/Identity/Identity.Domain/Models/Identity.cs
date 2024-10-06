namespace Domain.Models;

public class Identity
{
    public int Id { get; private set; }
    public Credentials Credentials { get; private set; }

    private Identity() { }
    public Identity(Credentials credentials)
    {
        Credentials = credentials;
    }
}