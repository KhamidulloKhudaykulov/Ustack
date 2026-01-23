using FluentValidation;

namespace UStack.Identity.Application.UseCases.Roles.CreateRole;

public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name cannot be empty")
            .MaximumLength(100).WithMessage("Role name is too long");
    }
}
