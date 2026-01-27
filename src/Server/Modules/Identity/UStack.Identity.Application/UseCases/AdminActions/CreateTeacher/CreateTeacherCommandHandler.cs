using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.UseCases.AdminActions.CreateTeacher;

public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand, Guid>
{
    private readonly CreateTeacherSaga _saga;

    public CreateTeacherCommandHandler(CreateTeacherSaga saga)
    {
        _saga = saga;
    }

    public async Task<Result<Guid>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        return await _saga.ExecuteAsync(request, cancellationToken);
    }
}
