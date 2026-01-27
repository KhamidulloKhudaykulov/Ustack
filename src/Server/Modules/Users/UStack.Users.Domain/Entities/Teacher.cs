using UStack.Users.Domain.Enums;
using UStack.Users.Domain.Interfaces;
using UStack.Users.Domain.Outcome;
using UStack.Users.Domain.Primitives;
using UStack.Users.Domain.States.Teachers;

namespace UStack.Users.Domain.Entities;

public class Teacher : Entity
{
    public Teacher(Guid id) : base(id) 
    {
        switch(State)
        {
            case UserState.Active:
                _state = new ActiveTeacherState();
                break;
            case UserState.Inactive:
                _state = new InactiveTeacherState();
                break;
            case UserState.Locked:
                _state = new LockedTeacherState();
                break;
            case UserState.Archived:
                _state = new ArchivedTeacherState();
                break;
            case UserState.Pending:
                _state = new PendingTeacherState();
                break;
        }
    }

    public Guid IdentityUserId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    private ITeacherStatusState? _state;

    public static Result<Teacher> Create(
        Guid identityUserId,
        string firstName,
        string lastName,
        string email,
        bool isActive = true)
    {
        var teacher = new Teacher(Guid.NewGuid())
        {
            IdentityUserId = identityUserId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            IsActive = isActive
        };

        teacher._state = new InactiveTeacherState();

        return teacher;
    }

    public void UpdateProfile(string firstName, string lastName, string email, bool isActive)
    {
        if (!string.IsNullOrWhiteSpace(firstName)) FirstName = firstName;
        if (!string.IsNullOrWhiteSpace(lastName)) LastName = lastName;
        if (!string.IsNullOrWhiteSpace(email)) Email = email;
        IsActive = isActive;
    }

    public UserState State { get; private set; } = UserState.Inactive;

    public void SetState(ITeacherStatusState state) => _state = state;
    public void ChangeState(UserState newState) => State = newState;

    public Result Activate() => _state!.Activate(this);
    public Result Deactivate() => _state!.Deactivate(this);
    public Result Lock() => _state!.Lock(this);
    public Result Archive() => _state!.Archive(this);
    public Result SetPending() => _state!.SetPending(this);
}
