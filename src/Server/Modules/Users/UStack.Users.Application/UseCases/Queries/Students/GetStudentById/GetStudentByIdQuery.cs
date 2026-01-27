using UStack.Users.Application.Abstraction.Messaging;

namespace UStack.Users.Application.UseCases.Queries.Students.GetStudentById;

public record GetStudentByIdQuery(Guid StudentId)
    : IQuery<StudentDto>;
