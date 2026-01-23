using FluentValidation.TestHelper;
using UStack.Identity.Application.UseCases.Users.CreateUser;
using Xunit;

namespace UStack.Identity.Tests.Validators;

public class CreateUserValidatorTests
{
    private readonly CreateUserValidator _validator;

    public CreateUserValidatorTests()
    {
        _validator = new CreateUserValidator();
    }

    [Fact]
    public void Should_Have_Failure_When_Username_Is_Empty()
    {
        var command = new CreateUserCommand("", "Password", "+99890080808");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.UserName)
                  .WithErrorMessage("UserName cannot be empty");
    }
}