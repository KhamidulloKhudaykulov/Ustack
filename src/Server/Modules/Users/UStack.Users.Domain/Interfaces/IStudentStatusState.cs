using UStack.Users.Domain.Entities;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Domain.Interfaces;

public interface IStudentStatusState
{
    Result Activate(Student student);
    Result Deactivate(Student student);
    Result Lock(Student student);
    Result Archive(Student student);
    Result SetPending(Student student);
}
