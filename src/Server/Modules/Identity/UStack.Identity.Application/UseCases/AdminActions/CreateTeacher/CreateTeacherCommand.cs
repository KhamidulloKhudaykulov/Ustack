using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.AdminActions.CreateTeacher;

public record CreateTeacherCommand(
        string Username,
        string Password,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        bool IsActive = true
    ) : ICommand<Guid>;