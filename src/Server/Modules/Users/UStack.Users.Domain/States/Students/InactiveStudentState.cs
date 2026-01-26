using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Students;

public class InactiveStudentState : IStudentStatusState
{
    public Result Activate(Student student)
    {
        student.SetState(new ActiveStudentState());
        student.ChangeState(UserState.Active);
        return Result.Success();
    }

    public Result Deactivate(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.AlreadyInactive",
            message: "This student is already inactive"
        ));
    }

    public Result Lock(Student student)
    {
        student.SetState(new LockedStudentState());
        student.ChangeState(UserState.Locked);
        return Result.Success();
    }

    public Result Archive(Student student)
    {
        student.SetState(new ArchivedStudentState());
        student.ChangeState(UserState.Archived);
        return Result.Success();
    }

    public Result SetPending(Student student)
    {
        student.SetState(new PendingStudentState());
        student.ChangeState(UserState.Pending);
        return Result.Success();
    }
}
