using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Features.Teachers.CreateTeacher;

public record CreateTeacherCommand(
        Guid IdentityUserId,
        string FirstName,
        string LastName,
        string Email,
        bool IsActive = true
    ) : ICommand<Guid>;