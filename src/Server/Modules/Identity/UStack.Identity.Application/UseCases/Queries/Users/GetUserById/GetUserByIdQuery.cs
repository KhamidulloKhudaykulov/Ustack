using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Queries.Users.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
