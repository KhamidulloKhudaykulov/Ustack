using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Infrastructure.BridgeServices;

public class CourseClientService : ICourseClient
{
    private readonly HttpClient _http;

    public CourseClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result> AssignTeacherToCourseAsync(
        Guid courseId,
        Guid teacherId,
        CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            return Result.Failure(new Error(
                code: "Name.Empty",
                message: "CourseId cannot be empty"));

        if (teacherId == Guid.Empty)
            return Result.Failure(new Error(
                code: "Id.Empty",
                message: "TeacherId cannot be empty"));

        var response = await _http.PatchAsync(
            $"api/courses/{courseId}/assign-teacher/{teacherId}",
            content: null,
            cancellationToken: ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(ct);
            return Result.Failure(new Error(
                code: response.StatusCode.ToString(),
                message: error));
        }

        return Result.Success();
    }
}
