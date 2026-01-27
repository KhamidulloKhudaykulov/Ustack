using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Primitives;
using UStack.Users.Domain.States.Students;

namespace UStack.Users.Domain.Entities;

public class Student : Entity
{
    public Student(Guid id)
        : base(id) 
    {
        switch(State)
        {
            case UserState.Active:
                _state = new ActiveStudentState();
                break;
            case UserState.Inactive:
                _state = new InactiveStudentState();
                break;
            case UserState.Locked:
                _state = new LockedStudentState();
                break;
            case UserState.Archived:
                _state = new ArchivedStudentState();
                break;
            case UserState.Pending:
                _state = new PendingStudentState();
                break;
        }
    }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    private IStudentStatusState? _state;

    public static Result<Student> Create(
        string firstName,
        string lastName,
        string email)
    {
        var student = new Student(Guid.NewGuid())
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        student._state = new PendingStudentState();

        return student;
    }

    public void UpdateProfile(
        string? firstName = null,
        string? lastName = null,
        string? email = null)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
            FirstName = firstName;

        if (!string.IsNullOrWhiteSpace(lastName))
            LastName = lastName;

        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
    }


    public UserState State { get; private set; } = UserState.Inactive;
    
    public void SetState(IStudentStatusState state) => _state = state;
    public void ChangeState(UserState newState) => State = newState;
    
    public Result Activate() => _state!.Activate(this);
    public Result Deactivate() => _state!.Deactivate(this);
    public Result Lock() => _state!.Lock(this);
    public Result Archive() => _state!.Archive(this);
    public Result SetPending() => _state!.SetPending(this);
}
