using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.Interfaces;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public class ConfirmResetPasswordTokenCommandHandler : ICommandHandler<ConfirmResetPasswordTokenCommand>
{
    private readonly IInMemoryCacheStorage _memoryCacheStorage;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmResetPasswordTokenCommandHandler(IInMemoryCacheStorage memoryCacheStorage, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _memoryCacheStorage = memoryCacheStorage;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ConfirmResetPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _memoryCacheStorage.GetString($"rp:{request.Email}");
        if (token == null || token != request.Token)
            return Result.Failure(new Error("ResetPassword.InvalidToken", "The reset password token is invalid or has expired."));

        var user = await _userRepository.SelectByUserNameAsync(request.Email, cancellationToken);
        if (user is null)
            return Result.Failure(new Error("User.NotFound", "User not found."));

        user.ResetPassword(request.NewPassword);
        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _memoryCacheStorage.Remove($"rp:{request.Email}");

        return Result.Success();
    }
}
