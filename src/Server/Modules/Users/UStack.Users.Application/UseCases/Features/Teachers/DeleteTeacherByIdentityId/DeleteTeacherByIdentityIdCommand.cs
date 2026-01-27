using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Features.Teachers.DeleteTeacher;

public record DeleteTeacherByIdentityIdCommand(
    Guid IdentityId) : ICommand;