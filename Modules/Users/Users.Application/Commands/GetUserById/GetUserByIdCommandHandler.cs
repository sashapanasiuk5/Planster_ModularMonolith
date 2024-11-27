using FluentResults;
using MediatR;
using User.Application.Mappers;
using Users.Contracts.Dto;
using Users.Infrastructure.Persistence.Repositories;

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
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.Fail($"User with id {request.UserId} not found");
        }
        return Result.Ok(user.ToDto());
    }
}