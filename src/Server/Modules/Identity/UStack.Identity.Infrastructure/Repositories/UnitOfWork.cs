using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdentityDbContext _dbContext;

    public UnitOfWork(IdentityDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}
