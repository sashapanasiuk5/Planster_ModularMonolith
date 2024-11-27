using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Tasks;

namespace WorkOrganization.Application.Commands.UpdateTask;

public record UpdateTaskCommand(UpdateTaskDto task, int taskId) : IRequest<Result<Unit>>;