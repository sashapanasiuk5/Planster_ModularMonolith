using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Users.Contracts.Dto;

namespace Application.Commands.AddIdentity;

public record AddIdentityCommand(int UserId, NewUserDto dto) : IRequest<Result<SessionDto>>;