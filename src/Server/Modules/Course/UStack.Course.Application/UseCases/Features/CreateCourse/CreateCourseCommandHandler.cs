using UStack.Course.Domain.Repositories;
using UStack.Course.Domain.Outcome;
using UStack.Course.Application.Abstraction.Messaging;
using UStack.Course.Domain.Entities;

namespace UStack.Course.Application.UseCases.Features.CreateCourse;

public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var courseResult = CourseEntity.Create(request.Name, request.Description);

        if (await _repository.SelectByNameAsync(request.Name, cancellationToken) is not null)
            return Result.Failure<Guid>(new Error(
                code: "CourseName.Duplicate",
                message: $"A course with the name '{request.Name}' already exists."));

        if (courseResult.IsFailure)
            return Result.Failure<Guid>(courseResult.Error);

        await _repository.InsertAsync(courseResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(courseResult.Value.Id);
    }
}
