using FluentResults;
using MediatR;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Errors;

namespace WorkOrganization.Application.Commands.Sprints.UpdateSprint;

public class UpdateSprintCommandHandler: IRequestHandler<UpdateSprintCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSprintCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _unitOfWork.SprintRepository.GetByIdAsync(request.SprintId);
        if (sprint == null)
            return Result.Fail(new SprintNotFound(request.SprintId));
        
        sprint.Update(request.Sprint.Title, request.Sprint.StartDate, request.Sprint.EndDate);
        await _unitOfWork.SaveAsync();
        return Unit.Value;
    }
}