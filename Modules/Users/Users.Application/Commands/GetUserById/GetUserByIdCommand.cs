using FluentResults;
using MediatR;
using Users.Contracts.Dto;

namespace User.Application.Commands.GetUserById;

public record GetUserByIdCommand(int UserId): IRequest<Result<UserDto>>;