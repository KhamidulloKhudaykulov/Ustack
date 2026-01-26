using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Features.Students.UpdateStudent;

public class UpdateStudentProfileCommandHandler : ICommandHandler<UpdateStudentProfileCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentProfileCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.SelectByIdAsync(request.StudentId, cancellationToken);

        if (student is null)
            return Result.Failure(new Error(
                code: "Student.NotFound",
                message: $"Student with specified value id={request.StudentId} was not found"));

        student.UpdateProfile(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.Email
        );

        await _studentRepository.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
