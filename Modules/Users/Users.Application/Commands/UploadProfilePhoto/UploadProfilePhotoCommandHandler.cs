using FluentResults;
using MediatR;
using User.Application.Interfaces;
using Users.Domain.Enums;
using Users.Domain.Models;

namespace User.Application.Commands.UploadProfilePhoto;

public class UploadProfilePhotoCommandHandler: IRequestHandler<UploadProfilePhotoCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IThumbnailService _thumbnailService;

    public UploadProfilePhotoCommandHandler(IUnitOfWork unitOfWork, IThumbnailService thumbnailService)
    {
        _unitOfWork = unitOfWork;
        _thumbnailService = thumbnailService;
    }

    public async Task<Result<Unit>> Handle(UploadProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail("User does not exist");
        
        await _unitOfWork.UserRepository.DeleteProfilePhotoAsync(user.Id);

        var newPhoto = new ProfilePhoto()
        {
            Data = request.Photo.Data,
            FileName = request.Photo.FileName,
            MimeType = request.Photo.MimeType,
            User = user,
            UserId = user.Id,
            Resolution = PhotoResolution.Normal
        };
        user.AddPhoto(newPhoto);
        
        var thumbnailBytes = _thumbnailService.GetThumbnailImage(request.Photo.Data, 200, 200);
        var thumbnail = new ProfilePhoto()
        {
            Data = thumbnailBytes,
            Resolution = PhotoResolution.Low,
            FileName = "thumbnail_" + request.Photo.FileName,
            MimeType = "image/png",
            User = user,
            UserId = user.Id
        };
        user.AddPhoto(thumbnail);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(Unit.Value);
    }
}