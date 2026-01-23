using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Queries.Users.GetUserById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            return Result.Failure<UserResponse>(new Error("User.NotFound", "User not found"));

        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.PhoneNumber,
            user.IsActive,
            user.LastLoginAt,
            user.Roles.Select(r => r.Role.Name).ToList()
        );

        return Result.Success(response);
    }
}
