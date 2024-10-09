using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams;

namespace Teams.Application.Commands.UpdateProject;

public record UpdateProjectCommand(int ProjectId, CreateUpdateProjectDto Dto): IRequest<Result<ProjectDto>>;