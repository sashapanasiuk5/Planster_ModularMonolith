using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;

namespace WorkOrganization.Application.Commands.GetById;

public record GetTaskByIdCommand(int TaskId): IRequest<Result<TaskDto>>;