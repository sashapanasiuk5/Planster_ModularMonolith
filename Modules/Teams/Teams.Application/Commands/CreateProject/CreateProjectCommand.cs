using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams;

namespace Teams.Application.Commands.CreateProject;

public record CreateProjectCommand(CreateUpdateProjectDto dto, int OwnerId) : IRequest<Result<Unit>>;
