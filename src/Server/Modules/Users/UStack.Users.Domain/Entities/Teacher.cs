using UStack.Users.Domain.Primitives;

namespace UStack.Users.Domain.Entities;

public class Teacher : Entity
{
    public Teacher(Guid id) : base(id) { }

    public Guid IdentityUserId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string EmployeeNumber { get; private set; } = default!;
    public string Department { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    public static Teacher Create(
        Guid identityUserId,
        string firstName,
        string lastName,
        string email,
        string employeeNumber,
        string department,
        bool isActive = true)
    {
        var teacher = new Teacher(Guid.NewGuid())
        {
            IdentityUserId = identityUserId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            EmployeeNumber = employeeNumber,
            Department = department,
            IsActive = isActive
        };

        return teacher;
    }

    public void UpdateProfile(string firstName, string lastName, string email, string department, bool isActive)
    {
        if (!string.IsNullOrWhiteSpace(firstName)) FirstName = firstName;
        if (!string.IsNullOrWhiteSpace(lastName)) LastName = lastName;
        if (!string.IsNullOrWhiteSpace(email)) Email = email;
        if (!string.IsNullOrWhiteSpace(department)) Department = department;
        IsActive = isActive;
    }
}
