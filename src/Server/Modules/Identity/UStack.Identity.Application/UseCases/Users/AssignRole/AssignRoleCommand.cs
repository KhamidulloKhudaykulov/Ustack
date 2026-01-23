using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.AssignRole;

public record AssignRoleCommand(Guid UserId, Guid RoleId) : ICommand;
