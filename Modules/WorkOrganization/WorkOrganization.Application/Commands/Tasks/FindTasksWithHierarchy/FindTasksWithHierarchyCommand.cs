using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Tasks;

namespace WorkOrganization.Application.Commands.Tasks.FindTasksWithHierarchy;

public record FindTasksWithHierarchyCommand(int ProjectId, string SearchWord): IRequest<Result<List<TaskShortHierarchyDto>>>;