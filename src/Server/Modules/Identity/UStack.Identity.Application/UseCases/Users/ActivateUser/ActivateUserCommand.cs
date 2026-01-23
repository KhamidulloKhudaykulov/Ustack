using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.ActivateUser;

public record ActivateUserCommand(Guid UserId) : ICommand;
