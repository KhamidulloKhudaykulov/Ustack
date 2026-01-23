using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Roles.DeactivateRole;

public record DeactivateRoleCommand(Guid RoleId) : ICommand;
