namespace Teams.Domain.Models;

public class Member
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    private readonly List<ProjectMember> _projectMembers = new List<ProjectMember>();
    public IReadOnlyCollection<ProjectMember> ProjectMembers => _projectMembers.AsReadOnly();

    public Member(int id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void LeaveProject(ProjectMember projectMember)
    {
        _projectMembers.Remove(projectMember);
    }

    public void JoinProject(ProjectMember projectMember)
    {
        _projectMembers.Add(projectMember);
    }
}