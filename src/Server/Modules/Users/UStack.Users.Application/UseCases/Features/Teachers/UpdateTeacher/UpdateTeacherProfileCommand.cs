using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Features.Teachers.UpdateTeacher;

public record UpdateTeacherProfileCommand(
   Guid TeacherId,
   string FirstName,
   string LastName,
   string Email,
   string Department,
   bool IsActive
) : ICommand;
