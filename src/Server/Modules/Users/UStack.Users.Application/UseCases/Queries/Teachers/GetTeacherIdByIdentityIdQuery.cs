using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Queries.Teachers;

public record GetTeacherIdByIdentityIdQuery(
    Guid IdentityId
) : IQuery<Guid>;
