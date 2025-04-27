using FluentResults;
using MediatR;
using Users.Contracts.Dto;

namespace User.Application.Commands.UploadProfilePhoto;

public record UploadProfilePhotoCommand(int UserId, FileDto Photo): IRequest<Result<Unit>>;