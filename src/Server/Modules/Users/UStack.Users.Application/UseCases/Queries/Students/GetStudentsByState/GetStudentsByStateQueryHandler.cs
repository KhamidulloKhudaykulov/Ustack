using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Application.Abstraction.Pagination;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Queries.Students.GetStudentsByState;

public class GetStudentsByStateQueryHandler
 : IQueryHandler<GetStudentsByStateQuery, PaginatedList<StudentDto>>
{
    private readonly IStudentRepository _repository;

    public GetStudentsByStateQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PaginatedList<StudentDto>>> Handle(
        GetStudentsByStateQuery request,
        CancellationToken cancellationToken)
    {
        var (students, totalCount) =
            await _repository.GetByStatePagedAsync(
                request.State,
                request.Pagination.PageNumber,
                request.Pagination.PageSize);

        var items = students
            .Select(StudentDto.From)
            .ToList();

        var result = PaginatedList<StudentDto>.Create(
            items,
            totalCount,
            request.Pagination.PageNumber,
            request.Pagination.PageSize);

        return Result.Success(result);
    }
}
