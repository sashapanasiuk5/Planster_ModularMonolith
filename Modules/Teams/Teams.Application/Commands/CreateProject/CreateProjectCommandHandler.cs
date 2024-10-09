using FluentResults;
using MediatR;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;

namespace Teams.Application.Commands.CreateProject;

public class CreateProjectCommandHandler: IRequestHandler<CreateProjectCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(IUnitOfWork unit)
    {
        _unitOfWork = unit;
    }
    public async Task<Result<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.dto.ToProject();
        var owner = await _unitOfWork.MembersRepository.GetMemberByIdAsync(request.OwnerId);
        if (owner == null)
        {
            return Result.Fail("Cannot find owner");
        }

        var result = project.AddOwner(owner);
        if (result.IsSuccess)
        {
            _unitOfWork.ProjectsRepository.AddProject(project);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }

        return result;
    }
}