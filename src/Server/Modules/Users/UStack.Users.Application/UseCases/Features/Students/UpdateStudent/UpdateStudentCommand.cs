using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Features.Students.UpdateStudent;

public record UpdateStudentProfileCommand(
    Guid StudentId,
    string? FirstName = null,
    string? LastName = null,
    string? Email = null
) : ICommand;
