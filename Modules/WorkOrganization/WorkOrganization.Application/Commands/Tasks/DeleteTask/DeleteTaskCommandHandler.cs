using FluentResults;
using MediatR;
using WorkOrganization.Application.Interfaces;

namespace WorkOrganization.Application.Commands.DeleteTask;

public class DeleteTaskCommandHandler: IRequestHandler<DeleteTaskCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
        if(task is null)
            return Result.Fail("Task not found");
        _unitOfWork.TaskRepository.RemoveTask(task);
        await _unitOfWork.SaveAsync();
        return Result.Ok();
    }
}