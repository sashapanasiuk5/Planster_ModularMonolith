using MediatR;
using Shared.Contracts.Dto.Tasks;

namespace WorkOrganization.Application.Commands.Tasks.GetAllStatuses;

public record GetAllStatusesCommand(): IRequest<List<TaskStatusDto>>;