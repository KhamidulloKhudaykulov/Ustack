using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Application.Abstraction.Pagination;
using UStack.Users.Domain.Enums;

namespace UStack.Users.Application.UseCases.Queries.Students.GetStudentsByState;

public record GetStudentsByStateQuery(
UserState State,
    PaginationParams Pagination) 
    : IQuery<PaginatedList<StudentDto>>;
