using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.Abstraction.Pagination;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Queries.Roles
{
    public class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, PaginatedList<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<PaginatedList<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var pagination = request.Pagination ?? new PaginationParams();
            pagination.SetPageSize(pagination.PageSize);

            var (roles, totalCount) = await _roleRepository.GetPagedRolesAsync(pagination.PageNumber, pagination.PageSize, cancellationToken);

            var roleResponses = roles.Select(r => new RoleResponse(
                r.Id,
                r.Name
            )).ToList();

            var paginatedResult = PaginatedList<RoleResponse>.Create(roleResponses, totalCount, pagination.PageNumber, pagination.PageSize);

            return Result.Success(paginatedResult);
        }
    }

}
