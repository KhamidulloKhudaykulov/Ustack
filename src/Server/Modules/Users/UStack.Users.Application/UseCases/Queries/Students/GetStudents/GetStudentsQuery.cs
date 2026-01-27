using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Application.Abstraction.Pagination;

namespace UStack.Users.Application.UseCases.Queries.Students.GetStudents;

public record GetStudentsQuery(PaginationParams Pagination)
: IQuery<PaginatedList<StudentDto>>;
