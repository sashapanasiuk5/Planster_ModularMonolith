using FluentResults;
using Identity.Contracts.Dtos;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.IntegrationEvents;
using Shared.Contracts.ModulesInterfaces;
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
        var session = await _identityModule.AddNewIdentityAsync(request.dto, newUser.Id);
        await _bus.PublishAsync(new UserRegistered(
            Guid.NewGuid(),
            newUser.Id,
            newUser.FirstName,
            newUser.LastName,
            newUser.Email,
            request.dto.Password,
            newUser.ImageUrl
        ));
        return Result.Ok(session);
    }
}