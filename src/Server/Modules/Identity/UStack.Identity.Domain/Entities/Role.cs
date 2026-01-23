using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Primitives;

namespace UStack.Identity.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<UserRole> _users = new();
    public IReadOnlyCollection<UserRole> Users => _users.AsReadOnly();

    private Role(Guid id, string name, bool isActive)
        : base(id)
    {
        Name = name;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
    }

    public static Result<Role> Create(string name, bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Role>(
                new Error("Role.InvalidName", "Role name cannot be empty"));

        var role = new Role(Guid.NewGuid(), name, isActive);
        return Result.Success(role);
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(new Error("Role.AlreadyActive", "Role is already active"));

        IsActive = true;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(new Error("Role.AlreadyInactive", "Role is already inactive"));

        IsActive = false;
        return Result.Success();
    }

    public Result UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(new Error("Role.InvalidName", "Role name cannot be empty"));

        Name = name;
        return Result.Success();
    }
}
