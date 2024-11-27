using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;
using WorkOrganization.Application.Commands.GetById;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;

namespace WorkOrganization.Application.Commands.GetTaskById;

public class GetTaskByIdCommandHandler: IRequestHandler<GetTaskByIdCommand, Result<TaskDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<TaskDto>> Handle(GetTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdWithDetailsAsync(request.TaskId);
        if(task is null)
            return Result.Fail($"Task with id {request.TaskId} does not exist");
        
        return Result.Ok(task.ToDto());
    }
}