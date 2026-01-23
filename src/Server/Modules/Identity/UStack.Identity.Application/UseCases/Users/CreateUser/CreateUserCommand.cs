using UStack.Identity.Application.Abstraction.Messaging;

namespace UStack.Identity.Application.UseCases.Users.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Password,
    string PhoneNumber) : ICommand<CreateUserResponse>;
