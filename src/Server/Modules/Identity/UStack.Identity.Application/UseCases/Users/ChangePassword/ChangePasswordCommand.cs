using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string NewPassword) : ICommand;
