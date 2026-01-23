using FluentValidation;

namespace UStack.Identity.Application.UseCases.Roles.UpdateRoleName;

public class UpdateRoleNameValidator : AbstractValidator<UpdateRoleNameCommand>
{
    public UpdateRoleNameValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
