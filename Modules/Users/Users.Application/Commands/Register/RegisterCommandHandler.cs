using FluentResults;
using Infrastructure.Dto;
using Infrastructure.IntegrationEvents;
using Infrastructure.ModulesInterfaces;
using MediatR;
using Shared.Infrastructure.EventBus;
using Users.Infrastructure.Persistence.Repositories;

namespace User.Application.Commands.Register;
using User = Users.Domain.Models.User;
public class RegisterCommandHandler: IRequestHandler<RegisterCommand, Result<SessionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _bus;
    private readonly IIdentityModule _identityModule;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IEventBus bus, IIdentityModule identityModule)
    {
        _unitOfWork = unitOfWork;
        _bus = bus;
        _identityModule = identityModule;
    }
    public async Task<Result<SessionDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
       var newUser = new User(request.dto.FirstName, request.dto.LastName, request.dto.Email, request.dto.ImageUrl);
        _unitOfWork.UserRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync();
        var session = await _identityModule.AddNewIdentityAsync(request.dto);
        return Result.Ok(session);
    }
}