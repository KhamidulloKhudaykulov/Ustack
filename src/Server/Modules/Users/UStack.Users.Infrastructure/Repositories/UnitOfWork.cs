using UStack.Users.Domain.Repositories;

namespace UStack.Users.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly UsersDbContext _dbContext;

    public UnitOfWork(UsersDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}
