using FluentResults;
using MediatR;

namespace WorkOrganization.Application.Commands.Tasks.ChangeTaskStatus;

public record ChangeTaskStatusCommand(int TaskId, int StatusId) : IRequest<Result<Unit>>;
