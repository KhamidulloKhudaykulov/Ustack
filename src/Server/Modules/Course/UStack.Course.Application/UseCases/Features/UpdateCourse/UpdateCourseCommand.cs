using UStack.Course.Application.Abstraction.Messaging;

namespace UStack.Course.Application.UseCases.Features.UpdateCourse;

public record UpdateCourseCommand(
    Guid CourseId,
    string? Name = null,
    string? Description = null
) : ICommand;