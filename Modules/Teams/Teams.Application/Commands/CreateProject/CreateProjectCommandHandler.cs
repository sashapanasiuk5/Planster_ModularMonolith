using FluentResults;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.Dto.Teams;
using Shared.Contracts.IntegrationEvents;
using Shared.Contracts.ModulesInterfaces;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Enums;
using Teams.Domain.Models;

namespace Teams.Application.Commands.CreateProject;

public class CreateProjectCommandHandler: IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _bus;
    private readonly IIdentityModule _identityModule;

    public CreateProjectCommandHandler(IUnitOfWork unit, IEventBus bus, IIdentityModule identityModule)
    {
        _unitOfWork = unit;
        _bus = bus;
        _identityModule = identityModule;
    }
    public async Task<Result<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.dto.ToProject();
        var owner = await _unitOfWork.MembersRepository.GetMemberByIdAsync(request.OwnerId);
        if (owner == null)
        {
            return Result.Fail("Cannot find owner");
        }

        var result = project.AddOwner(owner);

        if (result.IsSuccess)
        {
            _unitOfWork.ProjectsRepository.AddProject(project);
            await _unitOfWork.SaveChangesAsync();
            await _bus.PublishAsync(new ProjectCreated(Guid.NewGuid(), project.ToDto()), cancellationToken);
            await _bus.PublishAsync(
                new NewTeamMemberInvited(
                    Guid.NewGuid(),
                    owner.Id,
                    project.Id,
                    owner.FirstName + " " + owner.LastName,
                    owner.Email));
            await _identityModule.UpdatePermissionsAsync(owner.Id, owner.ProjectMembers.Select(pm => pm.ToPermissionDto()).ToList());
            return Result.Ok(project.ToDto());
        }

        return result;
    }
}