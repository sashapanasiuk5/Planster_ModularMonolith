using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.GetMembers;

public class GetMembersQueryHandler: IRequestHandler<GetMembersQuery, Result<List<MemberDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMembersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<MemberDto>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectWithMembersById(request.ProjectId);
        if (project == null)
        {
            return Result.Fail(new ProjectNotFound(request.ProjectId)); 
        }
        return Result.Ok(project.Members.Select(x => x.ToMemberDto()).ToList());
    }
}