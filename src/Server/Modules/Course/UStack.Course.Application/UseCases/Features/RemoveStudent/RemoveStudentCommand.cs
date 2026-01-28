using UStack.Course.Application.Abstraction.Messaging;

namespace UStack.Course.Application.UseCases.Features.RemoveStudent;

public record RemoveStudentCommand(
    Guid CourseId,
    Guid StudentId
) : ICommand;