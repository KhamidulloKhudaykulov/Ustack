using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Roles.ActivateRole;

public record ActivateRoleCommand(Guid RoleId) : ICommand;
