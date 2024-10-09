using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler: IRequestHandler<UpdateProjectCommand, Result<ProjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<ProjectDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if(project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));
        
        project.UpdateProjectDescription(request.Dto.Name, request.Dto.Description);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(project.ToDto());
    }
}