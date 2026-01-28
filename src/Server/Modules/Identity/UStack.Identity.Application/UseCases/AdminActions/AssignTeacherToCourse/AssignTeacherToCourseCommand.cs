using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.AdminActions.AssignTeacherToCourse;

public record AssignTeacherToCourseCommand(
    Guid CourseId,
    Guid TeacherId) : ICommand;
