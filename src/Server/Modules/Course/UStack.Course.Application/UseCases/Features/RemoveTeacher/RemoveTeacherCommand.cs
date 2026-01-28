using UStack.Course.Application.Abstraction.Messaging;

namespace UStack.Course.Application.UseCases.Features.RemoveTeacher;

public record RemoveTeacherCommand(
Guid CourseId,
Guid TeacherId
) : ICommand;
