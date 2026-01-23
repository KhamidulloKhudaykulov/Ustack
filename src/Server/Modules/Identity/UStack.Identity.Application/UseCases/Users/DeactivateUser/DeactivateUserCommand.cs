using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.DeactivateUser;

public record DeactivateUserCommand(Guid UserId) : ICommand;
