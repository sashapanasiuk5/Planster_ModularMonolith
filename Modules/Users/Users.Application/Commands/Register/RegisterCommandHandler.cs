using FluentResults;
using Identity.Contracts.Dtos;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.IntegrationEvents;
using Shared.Contracts.ModulesInterfaces;
using Users.Contracts.Dto;
using Users.Domain.Models;
using Users.Infrastructure.Persistence.Repositories;

namespace User.Application.Commands.Register;
using User = Users.Domain.Models.User;
public class RegisterCommandHandler: IRequestHandler<RegisterCommand, Result<UserRegisteredDto>>
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
    public async Task<Result<UserRegisteredDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
       var newUser = new User(request.dto.FirstName, request.dto.LastName, request.dto.Email, request.dto.ImageUrl, request.dto.Location);

       foreach (var contact in request.dto.Contacts)
       {
           var result = newUser.AddContact(new Contact()
           {
               Type = contact.Type,
               Value = contact.Link
           });
           if(result.IsFailed)
               return result.ToResult<UserRegisteredDto>();
       }
       
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
        return Result.Ok(new UserRegisteredDto()
        {
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            Session = session
        });
    }
}