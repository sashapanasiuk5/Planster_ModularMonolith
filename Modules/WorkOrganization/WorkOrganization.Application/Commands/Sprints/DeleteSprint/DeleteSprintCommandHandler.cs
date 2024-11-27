using FluentResults;
using MediatR;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Errors;

namespace WorkOrganization.Application.Commands.Sprints.DeleteSprint;

public class DeleteSprintCommandHandler: IRequestHandler<DeleteSprintCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSprintCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(DeleteSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _unitOfWork.SprintRepository.GetByIdAsync(request.SprintId);
        if (sprint == null)
            return Result.Fail(new SprintNotFound(request.SprintId));
        
        _unitOfWork.SprintRepository.Remove(sprint);
        await _unitOfWork.SaveAsync();
        return Unit.Value;
    }
}