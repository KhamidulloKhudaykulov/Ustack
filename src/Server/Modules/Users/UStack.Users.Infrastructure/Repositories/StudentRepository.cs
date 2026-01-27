using Microsoft.EntityFrameworkCore;
using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly UsersDbContext _dbContext;
    private readonly DbSet<Student> _students;

    public StudentRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
        _students = _dbContext.Set<Student>();
    }

    public async Task InsertAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _students.AddAsync(student, cancellationToken);
    }

    public async Task<Student?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Student?> SelectByStudentNameAsync(string studentName, CancellationToken cancellationToken = default)
    {
        return await _students
            .FirstOrDefaultAsync(s => s.FirstName == studentName || s.LastName == studentName, cancellationToken);
    }

    public Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        _students.Update(student);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Student student, CancellationToken cancellationToken = default)
    {
        _students.Remove(student);
        return Task.CompletedTask;
    }

    public async Task<(List<Student> Students, int TotalCount)> GetPagedStudentsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _students.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var students = await query
            .OrderBy(s => s.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (students, totalCount);
    }

    public async Task<(List<Student> Items, int TotalCount)> GetByStatePagedAsync(
        UserState state, 
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _students.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var students = await query
            .Where(s => s.State == state)
            .OrderBy(s => s.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (students, totalCount);
    }
}
