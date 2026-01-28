using UStack.Course.Application.Abstraction.Messaging;

namespace UStack.Course.Application.UseCases.Features.AssignTeacher;

public record AssignTeacherCommand(
    Guid CourseId,
    Guid TeacherId
) : ICommand;