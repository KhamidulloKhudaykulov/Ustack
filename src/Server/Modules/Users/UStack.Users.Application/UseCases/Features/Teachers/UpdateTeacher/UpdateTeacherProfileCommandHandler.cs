using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Features.Teachers.UpdateTeacher;

public class UpdateTeacherProfileCommandHandler : ICommandHandler<UpdateTeacherProfileCommand>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeacherProfileCommandHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTeacherProfileCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.SelectByIdAsync(request.TeacherId, cancellationToken);

        if (teacher is null)
            return Result.Failure(new Error(
                code: "Teacher.NotFound",
                message: $"Teacher with specified id={request.TeacherId} was not found"
            ));

        teacher.UpdateProfile(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.Email,
            isActive: request.IsActive
        );

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
