using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;

namespace UStack.Users.Application.UseCases.Queries.Students;

public class StudentDto
{
    public Guid Id { get; init; }
    public Guid IdentityUserId { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public UserState State { get; init; }

    public static StudentDto From(Student student) =>
        new()
        {
            Id = student.Id,
            IdentityUserId = student.IdentityUserId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            State = student.State
        };
}
