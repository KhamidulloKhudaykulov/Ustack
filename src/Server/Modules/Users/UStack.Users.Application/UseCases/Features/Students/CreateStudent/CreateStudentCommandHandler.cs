using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Repositories;

namespace UStack.Users.Application.UseCases.Features.Students.CreateStudent
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = Student.Create(
                    request.FirstName,
                    request.LastName,
                    request.Email);

            if (student.IsFailure)
                return Result.Failure<Guid>(student.Error);

            await _studentRepository.InsertAsync(student.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(student.Value.Id);
        }
    }
}
