using FluentValidation;

namespace UStack.Identity.Application.UseCases.Queries.Users.GetUsersWithRoles;

public class GetUsersWithRolesValidator : AbstractValidator<GetUsersWithRolesQuery>
{
    public GetUsersWithRolesValidator()
    {
        RuleFor(x => x.Pagination.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

        RuleFor(x => x.Pagination.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0");
    }
}
