using MediatR;
using System.Threading;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Enums;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.AdminActions.CreateTeacher;

public class CreateTeacherSaga
{
    private readonly IUserClient _userClient;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeacherSaga(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserClient userClient, IRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userClient = userClient;
        _roleRepository = roleRepository;
    }

    public async Task<Result<Guid>> ExecuteAsync(CreateTeacherCommand cmd, CancellationToken ct)
    {
        var newId = Guid.NewGuid();
        var teacherIdentity = User.Create(newId, cmd.Username, cmd.Password, cmd.PhoneNumber, isActive: true);
        if (teacherIdentity.IsFailure)
            return Result.Failure<Guid>(teacherIdentity.Error);

        var response = await _userClient.CreateTeacher(newId, cmd.FirstName, cmd.LastName, cmd.Email);
        if (response.IsFailure)
            return Result.Failure<Guid>(response.Error);

        var roleName = RoleName.Teacher.ToString().ToLower();
        var role = await _roleRepository.SelectByNameAsync("test");

        if (role is null)
        {
            await _userClient.RollBackCreateTeacher(newId);
            return Result.Failure<Guid>(new Error(
                code: "Role.NotFound",
                message: $"Role '{roleName}' not found"));
        }

        var assignRoleResult = teacherIdentity.Value.AssignRole(role);

        if (assignRoleResult.IsFailure)
        {
            await _userClient.RollBackCreateTeacher(newId);
            return Result.Failure<Guid>(assignRoleResult.Error);
        }

        try
        {
            await _userRepository.InsertAsync(teacherIdentity.Value, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
        catch(Exception ex)
        {
            await _userClient.RollBackCreateTeacher(newId);
            return Result.Failure<Guid>(new Error(
                code: "User.CreationFailed",
                message: $"Failed to create user identity: {ex.Message}"));
        }

        return Result.Success(newId);
    }
}
