using FluentValidation;

namespace UStack.Identity.Application.UseCases.Users.DeactivateUser;

public class DeactivateUserValidator : AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty");
    }
}
