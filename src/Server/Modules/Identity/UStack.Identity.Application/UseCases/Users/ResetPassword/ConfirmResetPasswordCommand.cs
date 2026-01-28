using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public record ConfirmResetPasswordCommand(
    string Email,
    string NewPassword) : ICommand;
