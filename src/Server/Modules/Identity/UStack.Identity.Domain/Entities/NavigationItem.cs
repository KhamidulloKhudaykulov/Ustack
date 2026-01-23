using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Primitives;

namespace UStack.Identity.Domain.Entities;

public class NavigationItem : Entity
{
    public string Title { get; private set; } = default!;
    public string Route { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private readonly List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private NavigationItem(Guid id, string title, string route, bool isActive) : base(id)
    {
        Title = title;
        Route = route;
        IsActive = isActive;
    }

    // Factory method
    public static Result<NavigationItem> Create(string title, string route, bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<NavigationItem>(new Error("NavigationItem.InvalidTitle", "Title cannot be empty"));

        if (string.IsNullOrWhiteSpace(route))
            return Result.Failure<NavigationItem>(new Error("NavigationItem.InvalidRoute", "Route cannot be empty"));

        return Result.Success(new NavigationItem(Guid.NewGuid(), title, route, isActive));
    }

    // Role-based access methods
    public Result AssignRole(Role role)
    {
        if (role == null)
            return Result.Failure(new Error("NavigationItem.InvalidRole", "Role cannot be null"));

        if (_roles.Any(r => r.Id == role.Id))
            return Result.Failure(new Error("NavigationItem.RoleAlreadyAssigned", "Role already has access"));

        _roles.Add(role);
        return Result.Success();
    }

    public Result RemoveRole(Role role)
    {
        var existing = _roles.FirstOrDefault(r => r.Id == role.Id);
        if (existing == null)
            return Result.Failure(new Error("NavigationItem.RoleNotAssigned", "Role does not have access"));

        _roles.Remove(existing);
        return Result.Success();
    }

    public bool CanBeAccessedBy(Role role)
    {
        return IsActive && _roles.Any(r => r.Id == role.Id);
    }
}
