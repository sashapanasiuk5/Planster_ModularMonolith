using FluentResults;
using MediatR;
using WorkOrganization.Application.Interfaces;

namespace WorkOrganization.Application.Commands.Tasks.ChangeTaskStatus;

public class ChangeTaskStatusCommandHandler: IRequestHandler<ChangeTaskStatusCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeTaskStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
        if(task is null)
            return Result.Fail("Task not found");
        
        var status = await _unitOfWork.TaskRepository.GetStatusByIdAsync(request.StatusId);
        if(status is null)
            return Result.Fail("Status not found");
        
        task.ChangeStatus(status);

        await _unitOfWork.SaveAsync();
        return Result.Ok();
    }
}