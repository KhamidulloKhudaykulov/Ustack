namespace UStack.Identity.Application.UseCases.Roles.CreateRole;

public record CreateRoleResponse(
     Guid RoleId,
     string Name,
     bool IsActive,
     DateTime CreatedAt
);
