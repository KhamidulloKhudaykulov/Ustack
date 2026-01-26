using UStack.Users.Application.Abstraction.Messaging;
using UStack.Users.Domain.Enums;

namespace UStack.Users.Application.UseCases.Features.Users;

public record ChangeUserStateCommand(
    Guid UserId,
    UserState NewState
) : ICommand;
