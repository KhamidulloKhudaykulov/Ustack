using UStack.Identity.Domain.Entities;

namespace UStack.Identity.Domain.Repositories;

public interface IRoleRepository
{
    Task InsertAsync(Role role, CancellationToken cancellationToken = default);
    Task<Role?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Role?> SelectByNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task UpdateAsync(Role role, CancellationToken cancellationToken = default);
    Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
    Task<(List<Role> Roles, int TotalCount)> GetPagedRolesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
