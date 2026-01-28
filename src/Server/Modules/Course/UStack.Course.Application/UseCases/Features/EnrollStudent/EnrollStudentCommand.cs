using UStack.Course.Application.Abstraction.Messaging;

namespace UStack.Course.Application.UseCases.Features.EnrollStudent;

public record EnrollStudentCommand(
 Guid CourseId,
 Guid StudentId
) : ICommand;
