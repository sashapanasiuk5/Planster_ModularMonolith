using MediatR;
using Shared.Contracts.IntegrationEvents;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.IntegrationEventsHandlers;

public class ProjectCreatedHandler: INotificationHandler<ProjectCreated>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectCreatedHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(ProjectCreated notification, CancellationToken cancellationToken)
    {
        var project = new Project(notification.Project.Name, notification.Project.Id);
        _unitOfWork.ProjectRepository.Add(project);
        await _unitOfWork.SaveAsync();
    }
}