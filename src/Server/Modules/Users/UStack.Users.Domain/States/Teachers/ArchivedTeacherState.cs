using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Teachers;

public class ArchivedTeacherState : ITeacherStatusState
{
    public Result Activate(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be activated"
        ));
    }

    public Result Deactivate(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be deactivated"
        ));
    }

    public Result Lock(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be locked"
        ));
    }

    public Result Archive(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.AlreadyArchived",
            message: "This student is already archived"
        ));
    }

    public Result SetPending(Teacher teacher)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be set to pending"
        ));
    }
}
