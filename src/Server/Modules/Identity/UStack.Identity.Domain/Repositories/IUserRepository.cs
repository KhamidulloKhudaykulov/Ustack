using UStack.Identity.Domain.Entities;

namespace UStack.Identity.Domain.Repositories;

public interface IUserRepository
{
    Task InsertAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> SelectByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
    Task<(List<User> Users, int TotalCount)> GetPagedUsersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
