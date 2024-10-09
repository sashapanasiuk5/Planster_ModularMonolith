using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;
using Teams.Domain.Models;

namespace Teams.Application.Commands.GetMemberProjects;

public class GetMemberProjectsCommandHandler: IRequestHandler<GetMemberProjectsCommand, Result<List<ProjectDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberProjectsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<ProjectDto>>> Handle(GetMemberProjectsCommand request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MembersRepository.GetMemberByIdWithProjectsAsync(request.MemberId);
        if(member is null)
            return Result.Fail(new MemberNotFound(request.MemberId));

        var projects = member.ProjectMembers.Select(x => x.Project.ToDto());
        return Result.Ok(projects.ToList());
    }
}