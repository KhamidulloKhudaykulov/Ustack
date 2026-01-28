using UStack.Course.Application.Abstraction.Messaging;

namespace UStack.Course.Application.UseCases.Features.CreateCourse;

public record CreateCourseCommand(
string Name,
string Description
) : ICommand<Guid>;
