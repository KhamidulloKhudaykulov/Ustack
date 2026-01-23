using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Primitives;

namespace UStack.Identity.Domain.Entities;

public class UserRole : Entity
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public User User { get; private set; } = default!;
    public Role Role { get; private set; } = default!;

    private UserRole(Guid id, User user, Role role) : base(id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        UserId = user.Id;
        RoleId = role.Id;
    }

    public static Result<UserRole> Create(User user, Role role)
    {
        if (user == null)
            return Result.Failure<UserRole>(new Error(
                "UserRole.InvalidUser",
                "User cannot be null"));

        if (role == null)
            return Result.Failure<UserRole>(new Error(
                "UserRole.InvalidRole",
                "Role cannot be null"));

        // Optional: Prevent duplicate role assignment at this level
        if (user.Roles.Any(r => r.RoleId == role.Id))
            return Result.Failure<UserRole>(new Error(
                "UserRole.AlreadyAssigned",
                "User already has this role"));

        var userRole = new UserRole(Guid.NewGuid(), user, role);

        return Result.Success(userRole);
    }
}
