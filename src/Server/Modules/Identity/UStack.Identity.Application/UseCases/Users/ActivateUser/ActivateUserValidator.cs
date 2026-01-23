using FluentValidation;

namespace UStack.Identity.Application.UseCases.Users.ActivateUser;
public class ActivateUserValidator : AbstractValidator<ActivateUserCommand>
{
    public ActivateUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty");
    }
}
