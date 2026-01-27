using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Application.Abstraction.Pagination;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Queries.Students.GetStudents;

public class GetStudentsQueryHandler
 : IQueryHandler<GetStudentsQuery, PaginatedList<StudentDto>>
{
    private readonly IStudentRepository _repository;

    public GetStudentsQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PaginatedList<StudentDto>>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.Pagination.PageNumber;
        var pageSize = request.Pagination.PageSize;

        var (students, totalCount) =
            await _repository.GetPagedStudentsAsync(pageNumber, pageSize);

        var items = students
            .Select(StudentDto.From)
            .ToList();

        var result = PaginatedList<StudentDto>.Create(
            items,
            totalCount,
            pageNumber,
            pageSize);

        return Result.Success(result);
    }
}
