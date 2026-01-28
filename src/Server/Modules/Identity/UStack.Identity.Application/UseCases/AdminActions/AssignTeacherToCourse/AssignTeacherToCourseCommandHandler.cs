using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.UseCases.AdminActions.AssignTeacherToCourse;

public class AssignTeacherToCourseCommandHandler : ICommandHandler<AssignTeacherToCourseCommand>
{
    private readonly IUserClient _userClient;
    private readonly ICourseClient _courseClient;

    public AssignTeacherToCourseCommandHandler(IUserClient userClient, ICourseClient courseClient)
    {
        _userClient = userClient;
        _courseClient = courseClient;
    }

    public async Task<Result> Handle(AssignTeacherToCourseCommand request, CancellationToken cancellationToken)
    {
        var teacherId = await _userClient.GetTeacherGuidByIdentityId(request.TeacherId);
        if (teacherId.Value == Guid.Empty)
            return Result.Failure(new Error("Teacher.NotFound", "Teacher not found for the given course."));
    
        var response = await _courseClient.AssignTeacherToCourseAsync(request.CourseId, teacherId.Value, cancellationToken);
        if (response.IsFailure)
            return Result.Failure(response.Error);

        return response;
    }
}
