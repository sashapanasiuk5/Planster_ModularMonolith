using MediatR;
using Shared.Contracts.IntegrationEvents;
using WorkOrganization.Application.Interfaces;

namespace WorkOrganization.Application.IntegrationEventsHandlers;

public class TeamMemberDeletedHandler: INotificationHandler<TeamMemberDeleted>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamMemberDeletedHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(TeamMemberDeleted notification, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(notification.ProjectId);
        var assignee = await _unitOfWork.AssigneeRepository.GetByIdAsync(notification.MemberId);
        project!.RemoveAssignee(assignee!);
        await _unitOfWork.SaveAsync();
    }
}