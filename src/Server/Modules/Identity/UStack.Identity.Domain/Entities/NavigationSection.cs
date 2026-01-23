using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Primitives;

namespace UStack.Identity.Domain.Entities;

public class NavigationSection : Entity
{
    public string Name { get; private set; } = default!;
    private readonly List<NavigationItem> _items = new();
    public IReadOnlyCollection<NavigationItem> Items => _items.AsReadOnly();

    private NavigationSection(Guid id, string name) : base(id)
    {
        Name = name;
    }

    // Factory method
    public static Result<NavigationSection> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<NavigationSection>(new Error("NavigationSection.InvalidName", "Name cannot be empty"));

        var section = new NavigationSection(Guid.NewGuid(), name);
        return Result.Success(section);
    }

    // Behavior methods
    public Result AddItem(NavigationItem item)
    {
        if (_items.Any(i => i.Id == item.Id))
            return Result.Failure(new Error("NavigationSection.ItemAlreadyAdded", "Item already exists in this section"));

        _items.Add(item);
        return Result.Success();
    }

    public Result RemoveItem(NavigationItem item)
    {
        if (!_items.Contains(item))
            return Result.Failure(new Error("NavigationSection.ItemNotFound", "Item not found in this section"));

        _items.Remove(item);
        return Result.Success();
    }
}

