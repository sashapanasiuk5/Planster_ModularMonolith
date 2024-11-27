using Identity.Contracts.Dtos;

namespace Users.Contracts.Dto;

public class UserRegisteredDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get;  set; }
    public required SessionDto Session { get; set; }
}