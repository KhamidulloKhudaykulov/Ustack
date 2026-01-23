using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Users.RemoveRole;

public class RemoveRoleCommandHandler : ICommandHandler<RemoveRoleCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRoleCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            return Result.Failure(new Error("User.NotFound", "User not found"));

        var role = await _roleRepository.SelectByIdAsync(request.RoleId, cancellationToken);
        if (role == null)
            return Result.Failure(new Error("UserRole.InvalidRole", "Role cannot be null"));

        var result = user.RemoveRole(role);
        if (result.IsFailure)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
