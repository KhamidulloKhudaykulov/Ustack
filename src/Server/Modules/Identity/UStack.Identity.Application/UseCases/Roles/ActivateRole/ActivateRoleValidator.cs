using FluentValidation;

namespace UStack.Identity.Application.UseCases.Roles.ActivateRole;

public class ActivateRoleValidator : AbstractValidator<ActivateRoleCommand>
{
    public ActivateRoleValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId cannot be empty");
    }
}
