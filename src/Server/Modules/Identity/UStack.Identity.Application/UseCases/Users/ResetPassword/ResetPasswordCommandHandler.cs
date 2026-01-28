using System;
using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Application.Interfaces;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly INotificationClient _notificationClient;
    private readonly IUserRepository _userRepository;
    private readonly IInMemoryCacheStorage _inMemoryCacheStorage;

    public ResetPasswordCommandHandler(INotificationClient notificationClient, IUserRepository userRepository, IInMemoryCacheStorage memoryCacheStorage)
    {
        _notificationClient = notificationClient;
        _userRepository = userRepository;
        _inMemoryCacheStorage = memoryCacheStorage;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = await _userRepository.SelectByUserNameAsync(request.Email);
        if (email is null)
            return Result.Failure(new Error("User.NotFound", "User not found"));

        var random = new Random();
        var token = random.Next(100000, 1000000).ToString();

        var existingToken = _inMemoryCacheStorage.GetString($"rp:{request.Email}");
        if (existingToken is not null)
        {
            await _notificationClient.SendEmailResetPasswordToken(email.UserName, existingToken);
            return Result.Success();
        }

        var response = await _notificationClient.SendEmailResetPasswordToken(email.UserName, token);
        if (response.IsFailure)
            return Result.Failure(response.Error);

        _inMemoryCacheStorage.SetString($"rp:{request.Email}", token, TimeSpan.FromMinutes(1));
        return Result.Success();
    }
}
