using UStack.Course.Application.Abstraction.Messaging;
using UStack.Course.Domain.Outcome;
using UStack.Course.Domain.Repositories;

namespace UStack.Course.Application.UseCases.Features.RemoveStudent;

public class RemoveStudentCommandHandler : ICommandHandler<RemoveStudentCommand>
{
    private readonly ICourseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStudentCommandHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.SelectByIdAsync(request.CourseId, cancellationToken);
        if (course == null)
            return Result.Failure(new Error("Course.NotFound", "Course not found"));

        course.RemoveStudent(request.StudentId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
