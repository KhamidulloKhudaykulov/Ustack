using Moq;
using UStack.Identity.Application.UseCases.Users.CreateUser;
using UStack.Identity.Domain.Entities;
using UStack.Identity.Domain.Repositories;
using Xunit;

namespace UStack.Identity.Tests.Features.Users;

public class CreateUserCommandTests
{
    [Fact]
    public async Task Handle_ValidCommand_CallsRepositoryInsert()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var command = new CreateUserCommand(
            UserName: "test.username",
            Password: "test.password",
            PhoneNumber: "+998900110011");
        
        var handler = new CreateUserCommandHandler(mockUserRepository.Object, mockUnitOfWork.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockUserRepository.Verify(r => r.InsertAsync(It.IsAny<User>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
