using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;

namespace Application.Commands.GetIdentityBySession;

public record GetSessionCommand(string sessionId) : IRequest<Result<SessionDto>>;