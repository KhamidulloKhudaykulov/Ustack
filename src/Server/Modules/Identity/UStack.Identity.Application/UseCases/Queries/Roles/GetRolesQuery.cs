using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.Abstraction.Pagination;

namespace UStack.Identity.Application.UseCases.Queries.Roles;

public record GetRolesQuery(PaginationParams Pagination) : IQuery<PaginatedList<RoleResponse>>;
