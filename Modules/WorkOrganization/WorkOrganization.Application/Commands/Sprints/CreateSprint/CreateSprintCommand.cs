using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Sprints;
using Shared.Contracts.EventBus;

namespace WorkOrganization.Application.Commands.Sprints.CreateSprint;

public record CreateSprintCommand(CreateSprintDto Sprint, int ProjectId): IRequest<Result<Unit>>;