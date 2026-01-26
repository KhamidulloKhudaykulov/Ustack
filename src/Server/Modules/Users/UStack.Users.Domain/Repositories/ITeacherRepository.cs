using UStack.Users.Domain.Entities;

namespace UStack.Users.Domain.Repositories;

public interface ITeacherRepository
{
    Task InsertAsync(Teacher teacher, CancellationToken cancellationToken = default);
    Task<Teacher?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Teacher?> SelectByTeacherNameAsync(string teacherName, CancellationToken cancellationToken = default);
    Task UpdateAsync(Teacher Student, CancellationToken cancellationToken = default);
    Task DeleteAsync(Teacher Student, CancellationToken cancellationToken = default);
    Task<(List<Teacher> Teachers, int TotalCount)> GetPagedTeachersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
