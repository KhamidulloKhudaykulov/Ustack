using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.Interfaces;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public class ConfirmResetTokenCommandHandler : ICommandHandler<ConfirmResetTokenCommand>
{
    private readonly IInMemoryCacheStorage _inMemoryCacheStorage;

    public ConfirmResetTokenCommandHandler(IInMemoryCacheStorage inMemoryCacheStorage)
    {
        _inMemoryCacheStorage = inMemoryCacheStorage;
    }

    public async Task<Result> Handle(ConfirmResetTokenCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"rp:{request.Email}";
        var storedToken = _inMemoryCacheStorage.GetString(cacheKey);
        if (storedToken == null || storedToken != request.Token)
            return Result.Failure(new Error("ResetPassword.InvalidToken", "The provided reset token is invalid or has expired."));
        
        _inMemoryCacheStorage.Remove(cacheKey);
        _inMemoryCacheStorage.SetString($"rp-ok:{request.Email}", "true", TimeSpan.FromMinutes(15));
        return await Task.FromResult(Result.Success());
    }
}
