using UStack.Course.Application.Abstraction.Messaging;
using UStack.Course.Domain.Outcome;
using UStack.Course.Domain.Repositories;

namespace UStack.Course.Application.UseCases.Features.AssignTeacher;

public class AssignTeacherCommandHandler : ICommandHandler<AssignTeacherCommand>
{
    private readonly ICourseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignTeacherCommandHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AssignTeacherCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.SelectByIdAsync(request.CourseId, cancellationToken);
        if (course == null)
            return Result.Failure(new Error("Course.NotFound", "Course not found"));

        var result = course.AssignTeacher(request.TeacherId);
        if (result.IsFailure)
            return result;

        await _repository.UpdateAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
