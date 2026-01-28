using UStack.Course.Domain.Repositories;

namespace UStack.Course.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CourseDbContext _dbContext;

    public UnitOfWork(CourseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }
}
