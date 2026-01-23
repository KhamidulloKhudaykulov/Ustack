using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Roles.UpdateRoleName;

public record UpdateRoleNameCommand(
     Guid RoleId,
     string Name) : ICommand;
