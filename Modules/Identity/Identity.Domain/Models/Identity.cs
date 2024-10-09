namespace Domain.Models;

public class Identity
{
    public int Id { get; private set; }
    public Credentials Credentials { get; private set; }


    private Identity() { }
    public Identity(Credentials credentials, int id = 0)
    {
        Credentials = credentials;
        Id = id;
    }
}