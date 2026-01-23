using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.Abstraction.Pagination;
using UStack.Identity.Application.UseCases.Queries.Users.GetUserById;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Queries.Users.GetUsersWithRoles;

public class GetUsersWithRolesQueryHandler : IQueryHandler<GetUsersWithRolesQuery, PaginatedList<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersWithRolesQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<PaginatedList<UserResponse>>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.Pagination ?? new PaginationParams();
        pagination.SetPageSize(pagination.PageSize);

        var (users, totalCount) = await _userRepository.GetPagedUsersAsync(pagination.PageNumber, pagination.PageSize, cancellationToken);

        var userResponses = users.Select(u => new UserResponse(
            u.Id,
            u.UserName,
            u.PhoneNumber,
            u.IsActive,
            u.LastLoginAt,
            u.Roles.Select(r => r.Role.Name).ToList()
        )).ToList();

        var paginatedResult = PaginatedList<UserResponse>.Create(userResponses, totalCount, pagination.PageNumber, pagination.PageSize);

        return Result.Success(paginatedResult);
    }
}
