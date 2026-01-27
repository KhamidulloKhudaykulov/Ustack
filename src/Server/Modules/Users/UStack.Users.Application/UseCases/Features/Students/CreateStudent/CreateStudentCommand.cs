using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Features.Students.CreateStudent;

public record CreateStudentCommand(
        string FirstName,
        string LastName,
        string Email) : ICommand<Guid>;