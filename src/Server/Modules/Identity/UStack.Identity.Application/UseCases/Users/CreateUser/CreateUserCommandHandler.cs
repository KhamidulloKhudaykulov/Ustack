using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = User.Create(Guid.NewGuid(), request.UserName, request.Password, request.PhoneNumber, true);

        if (userResult.IsFailure)
            return Result.Failure<CreateUserResponse>(userResult.Error);

        var user = userResult.Value!;

        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateUserResponse(
            user.Id,
            user.UserName,
            user.PhoneNumber,
            user.IsActive,
            DateTime.UtcNow
        );

        return Result.Success(response);
    }
}
