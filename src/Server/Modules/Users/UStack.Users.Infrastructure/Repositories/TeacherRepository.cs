using Microsoft.EntityFrameworkCore;
using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Teacher> _teachers;

    public TeacherRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _teachers = _dbContext.Set<Teacher>();
    }

    public async Task InsertAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await _teachers.AddAsync(teacher, cancellationToken);
    }

    public async Task<Teacher?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _teachers
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Teacher?> SelectByTeacherNameAsync(string teacherName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(teacherName)) return null;

        return await _teachers
            .FirstOrDefaultAsync(
                t => (t.FirstName + " " + t.LastName) == teacherName,
                cancellationToken);
    }

    public Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        _teachers.Update(teacher);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        _teachers.Remove(teacher);
        return Task.CompletedTask;
    }

    public async Task<(List<Teacher> Teachers, int TotalCount)> GetPagedTeachersAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _teachers.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var teachers = await query
            .OrderBy(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (teachers, totalCount);
    }
}