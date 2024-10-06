using FluentResults;
using Infrastructure.Dto;
using MediatR;

namespace User.Application.Commands.Register;

public record RegisterCommand(NewUserDto dto) : IRequest<Result<SessionDto>>;