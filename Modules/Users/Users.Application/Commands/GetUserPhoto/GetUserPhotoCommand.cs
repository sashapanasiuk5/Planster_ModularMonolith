using FluentResults;
using MediatR;
using Users.Contracts.Dto;
using Users.Domain.Enums;

namespace User.Application.Commands.GetUserPhoto;

public record GetUserPhotoCommand(int UserId, PhotoResolution Resolution): IRequest<Result<FileDto>>;