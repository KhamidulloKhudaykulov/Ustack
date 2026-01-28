namespace UStack.Course.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    //private readonly List<IDomainEvent> _domainEvents = new();
    //public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity(Guid id)
        => Id = id;

    //protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    //public void ClearDomainEvents() => _domainEvents.Clear();

    //public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => DomainEvents;
}