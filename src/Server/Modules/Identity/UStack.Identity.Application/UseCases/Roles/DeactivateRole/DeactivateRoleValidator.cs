using FluentValidation;

namespace UStack.Identity.Application.UseCases.Roles.DeactivateRole;
public class DeactivateRoleValidator : AbstractValidator<DeactivateRoleCommand>
{
    public DeactivateRoleValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId cannot be empty");
    }
}
