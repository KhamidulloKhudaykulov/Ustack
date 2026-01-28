namespace UStack.Identity.Application.UseCases.Users.LoginUser;

using FluentValidation;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
    }
}
