using FluentResults;
using MediatR;
using User.Application.Interfaces;
using User.Application.Mappers;
using Users.Contracts.Dto;

namespace User.Application.Commands.GetUserById;

public class GetUserByIdCommandHandler: IRequestHandler<GetUserByIdCommand, Result<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<UserDto>> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        Users.Domain.Models.User? user;
        if (request.IncludeContacts)
        {
            user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(request.UserId);
        }
        else
        {
            user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        }
        if (user == null)
        {
            return Result.Fail($"User with id {request.UserId} not found");
        }
        return Result.Ok(user.ToDto());
    }
}