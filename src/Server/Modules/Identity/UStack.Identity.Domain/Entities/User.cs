using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Primitives;

namespace UStack.Identity.Domain.Entities;

public class User : Entity
{
    private User(
        Guid id,
        string userName,
        string password,
        string phoneNumber,
        bool isActive) : base(id)
    {
        UserName = userName;
        Password = password;
        PhoneNumber = phoneNumber;
        IsActive = isActive;
    }
    public string UserName { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    // related entities
    private readonly List<UserRole> _roles = new();
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public static Result<User> Create(
        Guid id,
        string userName,
        string password,
        string phoneNumber,
        bool isActive)
    {
        var user = new User(
            id,
            userName,
            password,
            phoneNumber,
            isActive);

        return Result.Success(user);
    }

    public void ResetPassword(string newPassword)
        => Password = newPassword;

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(new Error(
                code: "User.AlreadyActive",
                message: "User is already active"));

        IsActive = true;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(new Error(
                code: "User.AlreadyInactive",
                message: "User is already inactive"));

        IsActive = false;
        return Result.Success();
    }

    public Result Login()
    {
        if (!IsActive)
            return Result.Failure(new Error(
                code: "User.Inactive",
                message: "Inactive user cannot login"));

        LastLoginAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            return Result.Failure(new Error(
                code: "User.InvalidPassword",
                message: "Password cannot be empty"));

        Password = newPassword;
        return Result.Success();
    }

    public Result AssignRole(Role role)
    {
        if (_roles.Any(r => r.RoleId == role.Id))
            return Result.Failure(new Error("UserRole.AlreadyAssigned", "User already has this role"));

        var userRoleResult = UserRole.Create(this, role);

        if (userRoleResult.IsFailure)
            return Result.Failure(userRoleResult.Error);

        _roles.Add(userRoleResult.Value!);

        return Result.Success();
    }

    public Result RemoveRole(Role role)
    {
        if (role == null)
            return Result.Failure(new Error("UserRole.InvalidRole", "Role cannot be null"));

        var userRole = _roles.FirstOrDefault(r => r.RoleId == role.Id);
        if (userRole == null)
            return Result.Failure(new Error("UserRole.NotAssigned", "User does not have this role"));

        _roles.Remove(userRole);

        return Result.Success();
    }

    public bool HasRole(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName)) return false;
        return _roles.Any(r => r.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }
}
