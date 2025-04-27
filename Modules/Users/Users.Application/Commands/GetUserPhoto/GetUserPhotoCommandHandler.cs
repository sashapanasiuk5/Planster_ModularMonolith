using FluentResults;
using MediatR;
using User.Application.Interfaces;
using Users.Contracts.Dto;

namespace User.Application.Commands.GetUserPhoto;

public class GetUserPhotoCommandHandler: IRequestHandler<GetUserPhotoCommand, Result<FileDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserPhotoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<FileDto>> Handle(GetUserPhotoCommand request, CancellationToken cancellationToken)
    {
        var photo = await _unitOfWork.UserRepository.GetProfilePhotoAsync(request.UserId, request.Resolution);
        if(photo == null)
            return Result.Fail("User not found");
        return Result.Ok(new FileDto()
        {
            Data = photo.Data,
            MimeType = photo.MimeType,
            FileName = photo.FileName
        });
    }
}