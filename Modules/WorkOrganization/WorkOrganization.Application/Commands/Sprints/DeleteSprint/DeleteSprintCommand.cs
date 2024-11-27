using FluentResults;
using MediatR;

namespace WorkOrganization.Application.Commands.Sprints.DeleteSprint;

public record DeleteSprintCommand(int SprintId) : IRequest<Result<Unit>>;