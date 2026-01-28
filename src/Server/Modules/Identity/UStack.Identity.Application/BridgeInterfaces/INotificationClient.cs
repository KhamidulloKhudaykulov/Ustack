using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.BridgeInterfaces;

public interface INotificationClient
{
    Task<Result> SendEmailResetPasswordToken(string email, string token);
}
