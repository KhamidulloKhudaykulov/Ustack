using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.Errors;

public static class TeacherError
{
    public static Error NotFound => new Error(
        code: "Teacher.NotFound",
        message: "Teacher with specified value was not found"
    );

    public static Error InvalidArgument => new Error(
        code: "Teacher.InvalidArgument",
        message: "Please enter valid argument"
    );
}
