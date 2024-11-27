using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.GetInvitationByCode;

public class GetInvitationByCodeQueryHandler: IRequestHandler<GetInvitationByCodeQuery, Result<InvitationInfoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInvitationByCodeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<InvitationInfoDto>> Handle(GetInvitationByCodeQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if (project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));
        
        var invitation = project.Invitations.FirstOrDefault(i => i.Code == request.Code);
        if(invitation == null)
            return Result.Fail("Invitation does not exist");
        return Result.Ok(new InvitationInfoDto()
        {
            ProjectId = project.Id,
            ProjectName = project.Name,
            Code = invitation.Code,
            Role = invitation.Role.ToString()
        });
    }
}