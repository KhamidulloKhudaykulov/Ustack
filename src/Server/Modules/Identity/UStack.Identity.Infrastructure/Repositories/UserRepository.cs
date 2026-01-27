using Microsoft.EntityFrameworkCore;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = _dbContext.Set<User>();
    }

    public async Task InsertAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> SelectByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _users
            .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> SelectByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        return await _users
            .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(
                u => u.UserName == userName,
                cancellationToken);
    }

    public Task UpdateAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        _users.Update(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        _users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task<(List<User> Users, int TotalCount)> GetPagedUsersAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _users
            .AsNoTracking()
            .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role);

        var totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .OrderBy(u => u.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (users, totalCount);
    }
}
