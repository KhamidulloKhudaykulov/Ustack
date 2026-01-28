using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Queries.Teachers;

public class GetTeacherIdByIdentityIdQueryHandler : IQueryHandler<GetTeacherIdByIdentityIdQuery, Guid>
{
    private readonly ITeacherRepository _teacherRepository;

    public GetTeacherIdByIdentityIdQueryHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<Guid>> Handle(GetTeacherIdByIdentityIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.SelectByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (teacher is null)
            return Result.Failure<Guid>(new Error("Teacher.NotFound", "Teacher not found"));

        return Result.Success(teacher.Id);
    }
}
