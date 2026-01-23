namespace UStack.Identity.Application.UseCases.Queries.Users.GetUserById;

using FluentValidation;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty");
    }
}
