using Application.Commands.Login;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.EventBus;

namespace Application.Handlers;

public class UserRegisteredIntegrationEventHandler: INotificationHandler<UserRegistered>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandler(IIdentityRepository identityRepository, IPasswordHasher hasher, ILogger<UserRegisteredIntegrationEventHandler> logger)
    {
        _identityRepository = identityRepository;
        _passwordHasher = hasher;
        _logger = logger;
    }
    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Hash(notification.Dto.Password);
        var creds = new Credentials(notification.Dto.Email, hashedPassword);
        var identity = new Domain.Models.Identity(creds);
        _identityRepository.Add(identity);
        await _identityRepository.SaveChangesAsync();
    }
}