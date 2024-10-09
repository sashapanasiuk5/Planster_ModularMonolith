namespace Shared.Contracts.Dto.Teams;

public class ProjectDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}