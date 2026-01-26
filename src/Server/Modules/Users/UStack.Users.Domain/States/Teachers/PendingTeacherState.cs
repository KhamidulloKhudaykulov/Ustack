using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Teachers;

public class PendingTeacherState : ITeacherStatusState
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
        return Result.Failure(new Error(
            code: "Student.AlreadyPending",
            message: "This student is already pending"
        ));
    }
}
