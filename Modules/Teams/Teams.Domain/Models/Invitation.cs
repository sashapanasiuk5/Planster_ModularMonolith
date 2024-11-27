using FluentResults;
using Teams.Domain.Enums;
using Teams.Domain.Errors;
using Teams.Domain.Interfaces;

namespace Teams.Domain.Models;

public class Invitation
{
    public const int CodeLength = 20;
    public int Id { get; private set; }
    public string Code { get; private set; }
    public int NumberOfPlaces { get; private set; }
    public int NumberOfInvitedUsers { get; private set; }
    public Project Project { get; private set; }
    public ProjectRole Role { get; private set; }

    private Invitation()
    {
        
    }
    public Invitation(Project project, ProjectRole role, int numberOfPlaces, IRandomStringGenerator randomStringGenerator)
    {
        Code = randomStringGenerator.Generate(CodeLength);
        NumberOfPlaces = numberOfPlaces;
        Role = role;
        Project = project;
    }

    public void Update(ProjectRole role, int numberOfPlaces)
    {
        Role = role;
        NumberOfPlaces = numberOfPlaces;
    }

    public Result<ProjectMember> Accept(Member invitedUser)
    {
        if (NumberOfPlaces > NumberOfInvitedUsers)
        {
            var result = Project.AddMember(invitedUser, Role);
            if (result.IsSuccess)
                NumberOfInvitedUsers++;
            return result;
        }
        return Result.Fail(new NumberOfPlacesIsOver());
    }
}