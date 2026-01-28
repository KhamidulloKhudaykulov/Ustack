using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string NewPassword) : ICommand;
