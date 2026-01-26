using Microsoft.EntityFrameworkCore;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Role> _roles;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _roles = _dbContext.Set<Role>();
    }

    public async Task InsertAsync(
        Role role,
        CancellationToken cancellationToken = default)
    {
        await _roles.AddAsync(role, cancellationToken);
    }

    public async Task<Role?> SelectByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _roles
            .Include(r => r.Users)
                .ThenInclude(ur => ur.User)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Role?> SelectByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _roles
            .Include(r => r.Users)
                .ThenInclude(ur => ur.User)
            .FirstOrDefaultAsync(
                r => r.Name == roleName,
                cancellationToken);
    }

    public Task UpdateAsync(
        Role role,
        CancellationToken cancellationToken = default)
    {
        _roles.Update(role);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        Role role,
        CancellationToken cancellationToken = default)
    {
        _roles.Remove(role);
        return Task.CompletedTask;
    }

    public async Task<(List<Role> Roles, int TotalCount)> GetPagedRolesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _roles
            .AsNoTracking()
            .Include(r => r.Users)
                .ThenInclude(ur => ur.User);

        var totalCount = await query.CountAsync(cancellationToken);

        var roles = await query
            .OrderBy(r => r.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (roles, totalCount);
    }
}
