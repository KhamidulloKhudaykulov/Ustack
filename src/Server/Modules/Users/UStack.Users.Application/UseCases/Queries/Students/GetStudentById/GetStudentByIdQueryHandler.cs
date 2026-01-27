using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Errors;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Queries.Students.GetStudentById;

public class GetStudentByIdQueryHandler
    : IQueryHandler<GetStudentByIdQuery, StudentDto>
{
    private readonly IStudentRepository _repository;

    public GetStudentByIdQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<StudentDto>> Handle(
        GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var student = await _repository.SelectByIdAsync(request.StudentId);

        if (student is null)
            return Result.Failure<StudentDto>(StudentError.NotFound);

        return Result.Success(StudentDto.From(student));
    }
}
