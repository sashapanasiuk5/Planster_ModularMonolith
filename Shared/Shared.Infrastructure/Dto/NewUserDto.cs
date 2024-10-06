namespace Infrastructure.Dto;

public class NewUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Email { get;  set; }
    public string? ImageUrl { get; private set; }
}