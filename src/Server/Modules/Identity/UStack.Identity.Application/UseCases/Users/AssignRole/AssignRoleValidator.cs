using FluentValidation;

namespace UStack.Identity.Application.UseCases.Users.AssignRole;
public class AssignRoleValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId cannot be empty");
    }
}
