namespace UStack.Identity.Application.UseCases.Users.CreateUser;

public record CreateUserResponse(
    Guid UserId,
    string UserName,
    string PhoneNumber,
    bool IsActive,
    DateTime CreatedAt);
