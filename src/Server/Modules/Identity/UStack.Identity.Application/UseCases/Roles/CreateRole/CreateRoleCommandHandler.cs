using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.Roles.CreateRole;

public class CreateRoleCommandHandler
: ICommandHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateRoleResponse>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var roleResult = Role.Create(request.Name, request.IsActive);
        if (roleResult.IsFailure)
            return Result.Failure<CreateRoleResponse>(roleResult.Error);

        var role = roleResult.Value!;

        await _roleRepository.InsertAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateRoleResponse(
            role.Id,
            role.Name,
            role.IsActive,
            role.CreatedAt
        ));
    }
}
