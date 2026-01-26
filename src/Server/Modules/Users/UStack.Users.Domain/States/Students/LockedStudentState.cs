using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Students;

public class LockedStudentState : IStudentStatusState
{
    public Result Activate(Student student)
    {
        student.SetState(new ActiveStudentState());
        student.ChangeState(UserState.Active);
        return Result.Success();
    }

    public Result Deactivate(Student student)
    {
        student.SetState(new InactiveStudentState());
        student.ChangeState(UserState.Inactive);
        return Result.Success();
    }

    public Result Lock(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.AlreadyLocked",
            message: "This student is already locked"
        ));
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
