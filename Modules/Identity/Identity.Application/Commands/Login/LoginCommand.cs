using System.Windows.Input;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;

namespace Application.Commands;

public record LoginCommand(LoginDto Dto) : IRequest<Result<SessionDto>>;