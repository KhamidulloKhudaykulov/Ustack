using FluentValidation;

namespace UStack.Identity.Application.UseCases.Queries.Roles;

public class GetRolesValidator : AbstractValidator<GetRolesQuery>
{
    public GetRolesValidator()
    {
        RuleFor(x => x.Pagination.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

        RuleFor(x => x.Pagination.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0");
    }
}
