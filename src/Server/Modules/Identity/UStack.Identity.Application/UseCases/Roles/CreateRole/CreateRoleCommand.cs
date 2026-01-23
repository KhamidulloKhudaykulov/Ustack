using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Roles.CreateRole;

public record CreateRoleCommand(
string Name,
bool IsActive
) : ICommand<CreateRoleResponse>;
