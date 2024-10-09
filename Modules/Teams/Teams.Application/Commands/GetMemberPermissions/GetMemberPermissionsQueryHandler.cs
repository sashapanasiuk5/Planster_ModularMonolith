using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.GetMemberRoles;

public class GetMemberPermissionsQueryHandler: IRequestHandler<GetMemberPermissionsQuery, Result<List<MemberPermissionDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberPermissionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<MemberPermissionDto>>> Handle(GetMemberPermissionsQuery request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MembersRepository.GetMemberByIdAsync(request.MemberId);
        if (member == null)
            return Result.Fail(new MemberNotFound(request.MemberId));
        return Result.Ok(member.ProjectMembers.Select(x => x.ToPermissionDto()).ToList());
    }
}