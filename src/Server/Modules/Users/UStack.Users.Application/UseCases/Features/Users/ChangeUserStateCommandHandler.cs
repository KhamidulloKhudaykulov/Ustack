using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Errors;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Features.Users;

public class ChangeUserStateCommandHandler : ICommandHandler<ChangeUserStateCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserStateCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork, ITeacherRepository teacherRepository)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
        _teacherRepository = teacherRepository;
    }

    public async Task<Result> Handle(ChangeUserStateCommand request, CancellationToken cancellationToken)
    {
        // Student qidiramiz, topilsa holatni o'zgartiramiz yoki error qaytaramiz. Topilmasa Teacher'dan qidiramiz.
        var student = await HandleStudentStateAsync(request, cancellationToken);
        if (student.IsSuccess)
            return student;

        switch(student.Error)
        {
            case var error when error == StudentError.NotFound:
                break;
            default:
                return student;
        }

        // Teacher qidiramiz. Bu ham topilmasa umumiy not found error qaytaramiz.
        var teacher = await HandleTeacherStateAsync(request, cancellationToken);
        if (teacher.IsSuccess)
            return teacher;
        
        switch(teacher.Error)
        {
            case var error when error == TeacherError.NotFound:
                break;
            default:
                return teacher;
        }

        // Har ikkalasi ham topilmadi va error qaytadi.
        return Result.Failure(new Error(
            code: "User.NotFound",
            message: $"User with id={request.UserId} was not found"
        ));
    }

    private async Task<Result> HandleStudentStateAsync(ChangeUserStateCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.SelectByIdAsync(request.UserId, cancellationToken);
        if (student is null)
            return Result.Failure(StudentError.NotFound);

        Result result = request.NewState switch
        {
            UserState.Active => student.Activate(),
            UserState.Inactive => student.Deactivate(),
            UserState.Locked => student.Lock(),
            UserState.Archived => student.Archive(),
            UserState.Pending => student.SetPending(),
            _ => Result.Failure(new Error(
                    code: "Student.InvalidState",
                    message: "The requested state is invalid"
                 ))
        };

        if (result.IsFailure)
            return result;

        await _studentRepository.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<Result> HandleTeacherStateAsync(ChangeUserStateCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.SelectByIdAsync(request.UserId, cancellationToken);
        if (teacher is null)
            return Result.Failure(TeacherError.NotFound);

        Result result = request.NewState switch
        {
            UserState.Active => teacher.Activate(),
            UserState.Inactive => teacher.Deactivate(),
            UserState.Locked => teacher.Lock(),
            UserState.Archived => teacher.Archive(),
            UserState.Pending => teacher.SetPending(),
            _ => Result.Failure(new Error(
                    code: "Teacher.InvalidState",
                    message: "The requested state is invalid"
                 ))
        };

        if (result.IsFailure)
            return result;

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
