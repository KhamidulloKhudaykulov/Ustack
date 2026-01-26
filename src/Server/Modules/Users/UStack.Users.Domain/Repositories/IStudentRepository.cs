using UStack.Users.Domain.Entities;

namespace UStack.Users.Domain.Repositories;

public interface IStudentRepository
{
    Task InsertAsync(Student Student, CancellationToken cancellationToken = default);
    Task<Student?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student?> SelectByStudentNameAsync(string StudentName, CancellationToken cancellationToken = default);
    Task UpdateAsync(Student Student, CancellationToken cancellationToken = default);
    Task DeleteAsync(Student Student, CancellationToken cancellationToken = default);
    Task<(List<Student> Students, int TotalCount)> GetPagedStudentsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
