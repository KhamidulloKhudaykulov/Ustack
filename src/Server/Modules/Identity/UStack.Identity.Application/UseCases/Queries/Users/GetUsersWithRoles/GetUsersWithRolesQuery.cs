using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.Abstraction.Pagination;
using UStack.Identity.Application.UseCases.Queries.Users.GetUserById;

namespace UStack.Identity.Application.UseCases.Queries.Users.GetUsersWithRoles;

public record GetUsersWithRolesQuery(PaginationParams Pagination) : IQuery<PaginatedList<UserResponse>>;
