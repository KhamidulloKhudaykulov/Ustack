using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.States.Students;

namespace UStack.Users.Domain.States.Teachers;

internal class ActiveTeacherState : ITeacherStatusState
{
    public Result Activate(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Teacher.AlreadyActive",
            message: "This Teacher is already active"
        ));
    }

    public Result Deactivate(Teacher teacher)
    {
        teacher.SetState(new InactiveTeacherState());
        teacher.ChangeState(UserState.Inactive);
        return Result.Success();
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
