using UStack.Identity.Application.Abstraction.Messaging;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Enums;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Domain.Repositories;

namespace UStack.Identity.Application.UseCases.AdminActions.CreateTeacher;

public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserClient _userClient;

    public CreateTeacherCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IUserRepository userRepository, IUserClient userClient)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userClient = userClient;
    }

    public async Task<Result<Guid>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var newId = Guid.NewGuid();
        var teacherIdentity = User.Create(newId, request.Username, request.Password, request.PhoneNumber, isActive: true);
        if (teacherIdentity.IsFailure)
            return Result.Failure<Guid>(teacherIdentity.Error);

        var response = await _userClient.CreateTeacher(newId, request.FirstName, request.LastName, request.Email);
        if (response.IsFailure)
            return Result.Failure<Guid>(response.Error);

        var roleName = RoleName.Teacher.ToString().ToLower();
        var role = await _roleRepository.SelectByNameAsync(roleName);
        teacherIdentity.Value.AssignRole(role!);
        await _userRepository.InsertAsync(teacherIdentity.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newId;
    }
}
