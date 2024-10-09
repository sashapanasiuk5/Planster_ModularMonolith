using FluentResults;
using Shared.Contracts.Errors;
using Teams.Domain.Enums;
using Teams.Domain.Errors;

namespace Teams.Domain.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    private List<ProjectMember> _members = new();
    private List<Invitation> _invitations = new();
    public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();
    public IReadOnlyCollection<Invitation> Invitations => _invitations.AsReadOnly();

    public Invitation? FindInvitationById(int id)
    {
        return _invitations.FirstOrDefault(i => i.Id == id);
    }

    public void UpdateProjectDescription(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void DeleteInvitation(Invitation invitation)
    {
        _invitations.Remove(invitation);
    }
    public Result AddOwner(Member newOwner)
    {
        var exsistingMember = _members.Find(m => m.MemberId == newOwner.Id);
        if (exsistingMember != null)
        {
            if (exsistingMember.Role != ProjectRole.Owner)
            {
                exsistingMember.ChangeRole(ProjectRole.Owner);
            }
            else
            {
                return Result.Fail("This member is already owner of this project.");
            }
        }
        else
        {
            _members.Add(new ProjectMember(Id, newOwner.Id, ProjectRole.Owner));
        }
        return Result.Ok();
    }

    public Result<ProjectMember> AddMember(Member newMember, ProjectRole role)
    {
        var exsistingMember = _members.Find(m => m.MemberId == newMember.Id);
        if (exsistingMember == null)
        {
            var newProjectMember = new ProjectMember(Id, newMember.Id, role);
            _members.Add(newProjectMember);
            return Result.Ok(newProjectMember);
        }
        return Result.Fail(new MemberAlreadyExists(newMember.Id));
    }

    public Result AddInvitation(Invitation newInvitation)
    {
        var invitationWithSameRole = _invitations.Find( inv => inv.Role == newInvitation.Role);
        if (invitationWithSameRole != null)
        {
            _invitations.Add(newInvitation);
            return Result.Ok();
        }
        return Result.Fail(new InvitationWithSameRoleExists(newInvitation.Role));
    }

    public Result RemoveInvitation(Invitation invitation)
    {
        if (_invitations.Contains(invitation))
        {
            _invitations.Remove(invitation);
            return Result.Ok();
        }

        return Result.Fail(new InvitationNotFound(invitation.Id));
    }

    public Result RemoveMember(Member member)
    {
        var prjectMember = _members.Find(m => m.MemberId == member.Id);
        if (prjectMember != null)
        {
            _members.Remove(prjectMember);
            member.LeaveProject(prjectMember);
            return Result.Ok();
        }
        return Result.Fail(new MemberNotFound(member.Id));
    }
}