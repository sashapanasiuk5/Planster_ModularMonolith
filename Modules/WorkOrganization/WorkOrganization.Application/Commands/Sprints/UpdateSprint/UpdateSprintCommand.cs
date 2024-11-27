using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Sprints;

namespace WorkOrganization.Application.Commands.Sprints.UpdateSprint;

public record UpdateSprintCommand(UpdateSprintDto Sprint, int SprintId): IRequest<Result<Unit>>;