using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Features.Teachers.CreateTeacher;

public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand, Guid>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = Teacher.Create(
            request.IdentityUserId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.IsActive
        );

        if (teacher.IsFailure)
            return Result.Failure<Guid>(teacher.Error);

        await _teacherRepository.InsertAsync(teacher.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(teacher.Value.Id);
    }
}