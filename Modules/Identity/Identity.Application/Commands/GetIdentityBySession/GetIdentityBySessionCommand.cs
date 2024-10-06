using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Commands.GetIdentityBySession;

public record GetIdentityBySessionCommand(string sessionId) : IRequest<Result<int>>;