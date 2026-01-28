using UStack.Course.Domain.Outcome;
using UStack.Course.Domain.Primitives;

namespace UStack.Course.Domain.Entities;

public class CourseEntity : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    // Teachers
    public IReadOnlyCollection<Guid> TeacherIds => _teacherIds.AsReadOnly();
    private List<Guid> _teacherIds = new();

    // Students
    public IReadOnlyCollection<Guid> StudentIds => _studentIds.AsReadOnly();
    private List<Guid> _studentIds = new();

    public bool IsActive { get; private set; } = true;
    public DateTime? UpdatedAt { get; private set; }

    private CourseEntity(Guid id) : base(id) { }

    public static Result<CourseEntity> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<CourseEntity>(new Error(
                code: "CourseName.Required",
                message: "Course name is required"));

        var course = new CourseEntity(Guid.NewGuid())
        {
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        return Result.Success(course);
    }

    // Update course info
    public void Update(string? name = null, string? description = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;

        if (!string.IsNullOrWhiteSpace(description))
            Description = description;

        UpdatedAt = DateTime.UtcNow;
    }

    // Teacher management
    public Result AssignTeacher(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            return Result.Failure<CourseEntity>(new Error(
                code: "Invalid.Argument",
                message: "Invalid TeacherId"));

        if (!_teacherIds.Contains(teacherId))
            _teacherIds.Add(teacherId);

        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result RemoveTeacher(Guid teacherId)
    {
        if (_teacherIds.Contains(teacherId))
        {
            _teacherIds.Remove(teacherId);
            UpdatedAt = DateTime.UtcNow;
        }

        return Result.Success();
    }

    // Student management
    public Result EnrollStudent(Guid studentId)
    {
        if (studentId == Guid.Empty)
            return Result.Failure<CourseEntity>(new Error(
                code: "Invalid.Argument",
                message: "Invalid StudentId"));

        if (!_studentIds.Contains(studentId))
            _studentIds.Add(studentId);

        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result RemoveStudent(Guid studentId)
    {
        if (_studentIds.Contains(studentId))
        {
            _studentIds.Remove(studentId);
            UpdatedAt = DateTime.UtcNow;
        }

        return Result.Success();
    }

    // Activate / Deactivate
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
