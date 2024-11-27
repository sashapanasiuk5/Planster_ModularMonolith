using MediatR;
using Shared.Contracts.Dto.Tasks;
using WorkOrganization.Application.Interfaces;

namespace WorkOrganization.Application.Commands.Tasks.GetAllStatuses;

public class GetAllStatusesCommandHandler: IRequestHandler<GetAllStatusesCommand, List<TaskStatusDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllStatusesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<TaskStatusDto>> Handle(GetAllStatusesCommand request, CancellationToken cancellationToken)
    {
        var statuses = await _unitOfWork.TaskRepository.GetAllStatusesAsync();
        return statuses.Select(x => new TaskStatusDto()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}