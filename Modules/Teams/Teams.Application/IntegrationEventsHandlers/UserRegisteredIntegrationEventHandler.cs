using MediatR;
using Shared.Contracts.IntegrationEvents;
using Teams.Application.Interfaces;
using Teams.Domain.Models;

namespace Teams.Application.IntegrationEventsHandlers;

public class UserRegisteredIntegrationEventHandler: INotificationHandler<UserRegistered>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRegisteredIntegrationEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var member = new Member(notification.UserId, notification.FirstName, notification.LastName, notification.Email, notification.ImageUrl);
        _unitOfWork.MembersRepository.AddMember(member);
        await _unitOfWork.SaveChangesAsync();
    }
}