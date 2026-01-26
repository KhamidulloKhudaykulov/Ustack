using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Teachers;

public class InactiveTeacherState : ITeacherStatusState
{
    public Result Activate(Teacher teacher)
    {
        teacher.SetState(new ActiveTeacherState());
        teacher.ChangeState(UserState.Active);
        return Result.Success();
    }

    public Result Deactivate(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.AlreadyInactive",
            message: "This student is already inactive"
        ));
    }

    public Result Lock(Teacher teacher)
    {
        teacher.SetState(new LockedTeacherState());
        teacher.ChangeState(UserState.Locked);
        return Result.Success();
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
