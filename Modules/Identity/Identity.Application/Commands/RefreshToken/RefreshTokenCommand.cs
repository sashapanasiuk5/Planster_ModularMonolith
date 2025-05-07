using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;

namespace Application.Commands.RefreshToken;

public record RefreshTokenCommand(string refreshToken, int identityId, int? projectId): IRequest<Result<LoginResultDto>>;