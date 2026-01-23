using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.RemoveRole;

public record RemoveRoleCommand(Guid UserId, Guid RoleId) : ICommand;
