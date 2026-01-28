using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public record ConfirmResetPasswordTokenCommand(
    string Email,
    string Token,
    string NewPassword) : ICommand;
