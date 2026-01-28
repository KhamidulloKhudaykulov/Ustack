using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.BridgeInterfaces;

public interface IUserClient
{
    Task<Result<bool>> CheckExistTeacherByEmail(string email);
    Task<Result<Guid>> CreateTeacher(
        Guid identityUserId,
        string firstName, 
        string lastName, 
        string email);

    Task<Result<Guid>> GetTeacherGuidByIdentityId(Guid identityUserId);

    Task<Result> RollBackCreateTeacher(Guid identityUserId);
}
