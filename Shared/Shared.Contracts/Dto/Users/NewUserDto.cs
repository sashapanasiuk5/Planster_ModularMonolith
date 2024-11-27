namespace Users.Contracts.Dto;

public class NewUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Email { get;  set; }
    public required string Location { get; set; }
    public string? ImageUrl { get; private set; }
    public List<ContactDto> Contacts { get; set; }
}