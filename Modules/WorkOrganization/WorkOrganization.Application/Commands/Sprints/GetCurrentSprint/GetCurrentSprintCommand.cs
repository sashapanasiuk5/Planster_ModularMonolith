using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Sprints;

namespace WorkOrganization.Application.Commands.Sprints.GetCurrentSprint;

public record GetCurrentSprintCommand(int ProjectId): IRequest<Result<SprintDto?>>;