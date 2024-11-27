using MediatR;
using Shared.Contracts.IntegrationEvents;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.IntegrationEventsHandlers;

public class UserRegisteredHandler: INotificationHandler<UserRegistered>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRegisteredHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var assignee = new Assignee(
            notification.UserId,
            notification.FirstName + " " + notification.LastName,
            notification.Email);
        _unitOfWork.AssigneeRepository.Add(assignee);
        await _unitOfWork.SaveAsync();
    }
}