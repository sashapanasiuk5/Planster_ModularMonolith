using MediatR;
using Shared.Contracts.Dto.Teams.Member;
using Shared.Contracts.ModulesInterfaces;
using Teams.Application.Commands.GetMemberRoles;

namespace Teams.Infrastructure;

public class TeamsModule: ITeamsModule
{
    private readonly IMediator _mediator;

    public TeamsModule(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<List<MemberPermissionDto>> GetMemberPermissionsAsync(int memberId)
    {
        var result = await _mediator.Send(new GetMemberPermissionsQuery(memberId));
        if(result.IsFailed)
            throw new Exception(result.Errors.First().Message);
        return result.Value;
    }
}