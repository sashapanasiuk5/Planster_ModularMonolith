using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;
using Shared.Contracts.Dto.Work.Tasks;

namespace WorkOrganization.Application.Commands.CreateTask;

public record CreateTaskCommand(CreateTaskDto task): IRequest<Result<Unit>>;