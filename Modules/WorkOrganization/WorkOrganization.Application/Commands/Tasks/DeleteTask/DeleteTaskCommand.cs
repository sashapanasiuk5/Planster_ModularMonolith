using FluentResults;
using MediatR;

namespace WorkOrganization.Application.Commands.DeleteTask;

public record DeleteTaskCommand(int TaskId): IRequest<Result<Unit>>;