namespace UStack.Identity.Application.UseCases.Users.LoginUser;

using FluentValidation;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty");
    }
}
