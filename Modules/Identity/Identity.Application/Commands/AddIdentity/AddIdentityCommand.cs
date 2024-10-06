using FluentResults;
using Infrastructure.Dto;
using MediatR;

namespace Application.Commands.AddIdentity;

public record AddIdentityCommand(NewUserDto dto) : IRequest<Result<SessionDto>>;