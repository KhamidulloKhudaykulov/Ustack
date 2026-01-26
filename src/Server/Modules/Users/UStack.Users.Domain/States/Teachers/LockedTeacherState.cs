using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Teachers;

public class LockedTeacherState : ITeacherStatusState
{
    public Result Activate(Teacher teacher)
    {
        teacher.SetState(new ActiveTeacherState());
        teacher.ChangeState(UserState.Active);
        return Result.Success();
    }

    public Result Deactivate(Teacher teacher)
    {
        teacher.SetState(new InactiveTeacherState());
        teacher.ChangeState(UserState.Inactive);
        return Result.Success();
    }

    public Result Lock(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.AlreadyLocked",
            message: "This student is already locked"
        ));
    }

    public Result Archive(Teacher teacher)
    {
        teacher.SetState(new ArchivedTeacherState());
        teacher.ChangeState(UserState.Archived);
        return Result.Success();
    }

    public Result SetPending(Teacher teacher)
    {
        teacher.SetState(new PendingTeacherState());
        teacher.ChangeState(UserState.Pending);
        return Result.Success();
    }
}
