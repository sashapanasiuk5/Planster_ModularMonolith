using Teams.Domain.Enums;

namespace Identity.Contracts.Dtos;

public class ProjectPermissions
{
    public int ProjectId { get; set; }
    public ProjectRole Role { get; set; }
}