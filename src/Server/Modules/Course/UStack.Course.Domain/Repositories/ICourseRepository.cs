using UStack.Course.Domain.Entities;

namespace UStack.Course.Domain.Repositories;

public interface ICourseRepository
{
    Task<CourseEntity> InsertAsync(CourseEntity course, CancellationToken ct);
    Task<CourseEntity> UpdateAsync(CourseEntity course, CancellationToken ct);
    Task DeleteAsync(CourseEntity course, CancellationToken ct);
    Task<CourseEntity?> SelectByIdAsync(Guid id, CancellationToken ct);
    Task<CourseEntity?> SelectByNameAsync(string name, CancellationToken ct);
    Task<(List<CourseEntity> Courses, int TotalCount)> GetPagedRolesAsync(int pageNumber, int pageSize, CancellationToken ct = default);
}
