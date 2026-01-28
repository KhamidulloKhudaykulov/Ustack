using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.ResetPassword;

public record ConfirmResetTokenCommand(
    string Email,
    string Token) : ICommand;
