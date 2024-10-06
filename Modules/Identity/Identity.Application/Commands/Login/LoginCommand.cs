using System.Windows.Input;
using FluentResults;
using Infrastructure.Dto;
using MediatR;

namespace Application.Commands;

public record LoginCommand(LoginDto Dto) : IRequest<Result<SessionDto>>;