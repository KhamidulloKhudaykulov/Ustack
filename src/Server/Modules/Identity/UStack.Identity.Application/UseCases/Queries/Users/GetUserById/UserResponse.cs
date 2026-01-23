namespace UStack.Identity.Application.UseCases.Queries.Users.GetUserById;

public record UserResponse(
    Guid UserId,
    string UserName,
    string PhoneNumber,
    bool IsActive,
    DateTime? LastLoginAt,
    List<string> Roles
);
