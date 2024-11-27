using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;

namespace WorkOrganization.Application.Commands.Tasks.FindTaskByTitle;

public record FindTaskByTitleCommand(int ProjectId, string SearchWord): IRequest<Result<List<TaskShortDto>>>;