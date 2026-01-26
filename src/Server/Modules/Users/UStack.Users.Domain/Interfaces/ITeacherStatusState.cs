using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.Interfaces;

public interface ITeacherStatusState
{
    Result Activate(Teacher teacher);
    Result Deactivate(Teacher teacher);
    Result Lock(Teacher teacher);
    Result Archive(Teacher teacher);
    Result SetPending(Teacher teacher);
}
