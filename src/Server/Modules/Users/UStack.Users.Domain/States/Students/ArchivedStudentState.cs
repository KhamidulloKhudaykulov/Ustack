using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.States.Students;

public class ArchivedStudentState : IStudentStatusState
{
    public Result Activate(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be activated"
        ));
    }

    public Result Deactivate(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be deactivated"
        ));
    }

    public Result Lock(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be locked"
        ));
    }

    public Result Archive(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.AlreadyArchived",
            message: "This student is already archived"
        ));
    }

    public Result SetPending(Student student)
    {
        return Result.Failure(new Error(
            code: "Student.Archived",
            message: "Archived student cannot be set to pending"
        ));
    }
}
