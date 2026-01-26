using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.Errors;

public static class StudentError
{
    public static Error NotFound => new Error(
        code: "Student.NotFound",
        message: "Student with specified value was not found"
    );

    public static Error InvalidArgument => new Error(
        code: "Student.InvalidArgument",
        message: "Please enter valid argument"
    );
}
