namespace Shared.Contracts.Dto.Teams.Member;

public class MemberDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public string Role { get; set; }
}