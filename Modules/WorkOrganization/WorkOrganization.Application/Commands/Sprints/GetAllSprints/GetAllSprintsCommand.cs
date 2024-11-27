using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Sprints;

namespace WorkOrganization.Application.Commands.Sprints.GetAllSprints;

public record GetAllSprintsCommand(int ProjectId): IRequest<Result<List<SprintShortDto>>>;