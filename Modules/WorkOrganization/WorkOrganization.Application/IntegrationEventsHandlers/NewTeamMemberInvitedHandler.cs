using MediatR;
using Shared.Contracts.IntegrationEvents;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.IntegrationEventsHandlers;

public class NewTeamMemberInvitedHandler: INotificationHandler<NewTeamMemberInvited>
{
    private readonly IUnitOfWork _unitOfWork;

    public NewTeamMemberInvitedHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(NewTeamMemberInvited notification, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(notification.ProjectId);
        var assignee = await _unitOfWork.AssigneeRepository.GetByIdAsync(notification.MemberId);
        project!.AddAssignee(assignee!);
        await _unitOfWork.SaveAsync();
    }
}