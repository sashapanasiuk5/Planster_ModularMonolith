using FluentResults;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.Dto.Teams;
using Shared.Contracts.IntegrationEvents;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;

namespace Teams.Application.Commands.CreateProject;

public class CreateProjectCommandHandler: IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _bus;

    public CreateProjectCommandHandler(IUnitOfWork unit, IEventBus bus)
    {
        _unitOfWork = unit;
        _bus = bus;
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
            return Result.Ok(project.ToDto());
        }

        return result;
    }
}