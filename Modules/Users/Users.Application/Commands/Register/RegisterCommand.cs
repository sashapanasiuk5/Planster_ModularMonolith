using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Users.Contracts.Dto;

namespace User.Application.Commands.Register;

public record RegisterCommand(NewUserDto dto) : IRequest<Result<UserRegisteredDto>>;