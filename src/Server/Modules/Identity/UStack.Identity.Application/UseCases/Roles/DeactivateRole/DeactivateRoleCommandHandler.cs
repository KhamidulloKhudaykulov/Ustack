using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Roles.DeactivateRole;

public class DeactivateRoleCommandHandler
: ICommandHandler<DeactivateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateRoleCommandHandler(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeactivateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.SelectByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
            return Result.Failure(new Error("Role.NotFound", "Role not found"));

        var result = role.Deactivate();
        if (result.IsFailure)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
