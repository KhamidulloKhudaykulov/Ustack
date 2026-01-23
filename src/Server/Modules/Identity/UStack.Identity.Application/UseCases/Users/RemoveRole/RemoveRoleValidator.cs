namespace UStack.Identity.Application.UseCases.Users.RemoveRole;

using FluentValidation;

public class RemoveRoleValidator : AbstractValidator<RemoveRoleCommand>
{
    public RemoveRoleValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId cannot be empty");
    }
}
