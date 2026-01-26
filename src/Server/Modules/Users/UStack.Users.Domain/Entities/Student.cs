using UStack.Users.Domain.Primitives;

namespace UStack.Users.Domain.Entities;

public class Student : Entity
{
    public Student(Guid id)
        : base(id) { }

    public Guid IdentityUserId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Student Create(
        Guid identityUserId,
        string firstName,
        string lastName,
        string email)
    {
        var Student = new Student(Guid.NewGuid())
        {
            IdentityUserId = identityUserId,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        return Student;
    }

    public void UpdateProfile(
        string? firstName = null,
        string? lastName = null,
        string? email = null)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
            FirstName = firstName;

        if (!string.IsNullOrWhiteSpace(lastName))
            LastName = lastName;

        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
    }
}
