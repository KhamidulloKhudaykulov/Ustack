using Microsoft.EntityFrameworkCore;
using UStack.Course.Domain.Entities;
using UStack.Course.Domain.Repositories;

namespace UStack.Course.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _dbContext;
    private readonly DbSet<CourseEntity> _courses;

    public CourseRepository(CourseDbContext dbContext)
    {
        _dbContext = dbContext;
        _courses = dbContext.Set<CourseEntity>();
    }

    public async Task<CourseEntity> InsertAsync(CourseEntity course, CancellationToken ct)
    {
        await _courses.AddAsync(course, ct);
        await _dbContext.SaveChangesAsync(ct);
        return course;
    }

    public async Task<CourseEntity> UpdateAsync(CourseEntity course, CancellationToken ct)
    {
        _courses.Update(course);
        await _dbContext.SaveChangesAsync(ct);
        return course;
    }

    public async Task DeleteAsync(CourseEntity course, CancellationToken ct)
    {
        _courses.Remove(course);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<CourseEntity?> SelectByIdAsync(Guid id, CancellationToken ct)
    {
        return await _courses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<CourseEntity?> SelectByNameAsync(string name, CancellationToken ct)
    {
        return await _courses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, ct);
    }

    public async Task<(List<CourseEntity> Courses, int TotalCount)> GetPagedRolesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var query = _courses.AsNoTracking();

        var totalCount = await query.CountAsync(ct);

        var courses = await query
            .OrderBy(x => x.CreatedAt) // agar yo‘q bo‘lsa Id bilan sort qil
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (courses, totalCount);
    }
}
