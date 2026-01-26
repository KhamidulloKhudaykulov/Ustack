using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Features.Teachers.CreateTeacher;

public record CreateTeacherCommand(
        Guid IdentityUserId,
        string FirstName,
        string LastName,
        string Email,
        string EmployeeNumber,
        string Department,
        bool IsActive = true
    ) : ICommand<Guid>;