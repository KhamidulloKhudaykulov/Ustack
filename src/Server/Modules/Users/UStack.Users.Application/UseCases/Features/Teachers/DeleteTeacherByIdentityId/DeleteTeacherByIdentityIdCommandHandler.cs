using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Application.UseCases.Features.Teachers.DeleteTeacher;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Features.Teachers.DeleteTeacherByIdentityId;

public class DeleteTeacherByIdentityIdCommandHandler : ICommandHandler<DeleteTeacherByIdentityIdCommand>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTeacherByIdentityIdCommandHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTeacherByIdentityIdCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.SelectByIdentityIdAsync(request.IdentityId);
        await _teacherRepository.DeleteAsync(teacher!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
