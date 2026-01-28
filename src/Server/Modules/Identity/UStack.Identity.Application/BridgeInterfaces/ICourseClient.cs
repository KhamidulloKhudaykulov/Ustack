using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.BridgeInterfaces;

public interface ICourseClient
{
    Task<Result> AssignTeacherToCourseAsync(Guid courseId, Guid teacherId, CancellationToken ct);
}
