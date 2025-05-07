using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;

namespace Application.Commands.SwitchCurrentProject;

public record SwitchCurrentProjectCommand(int identityId, int projectId): IRequest<Result<SwitchProjectDto>>;