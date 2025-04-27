using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Tasks;

namespace WorkOrganization.Application.Commands.Tasks.GetTasksOnBoard;

public record GetTasksOnBoardCommand(int  ProjectId): IRequest<Result<List<BoardTaskDto>>>;