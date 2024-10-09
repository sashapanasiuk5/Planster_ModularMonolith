using FluentResults;
using Teams.Domain.Enums;

namespace Teams.Domain.Models;

public class ProjectMember
{
    public int MemberId { get; private set; }
    public int ProjectId { get; private set; }
    public ProjectRole Role { get; private set; }
    public Project Project { get; private set; }
    public Member Member { get; private set; }

    public ProjectMember(Project project, Member member, ProjectRole role)
    {
        Member = member;
        Project = project;
        ProjectId = project.Id;
        MemberId = member.Id;
        Role = role;
    }
    
    public ProjectMember(int projectId, int memberId, ProjectRole role)
    {
        ProjectId = projectId;
        MemberId = memberId;
        Role = role;
    }

    public void ChangeRole(ProjectRole role)
    {
        Role = role;
    }
}