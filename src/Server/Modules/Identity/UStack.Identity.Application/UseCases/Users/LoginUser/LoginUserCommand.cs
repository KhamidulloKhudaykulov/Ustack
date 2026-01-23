using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.LoginUser;

public record LoginUserCommand(Guid UserId) : ICommand;
