using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;
using Shared.Contracts.Dto.Work.Tasks;

namespace WorkOrganization.Application.Commands.GetAllTasks;

public record GetAllTasksCommand(int ProjectId, TaskFilterDto? Filter): IRequest<Result<List<TaskHierarchyDto>>>;