using Xunit;
using Moq;
using CareerLens.Application.UseCases.Users;
using CareerLens.Application.Dtos.Users;
using CareerLens.Domain.Interfaces;
using CareerLens.Domain.Entities;

public class UserUseCaseTests
{
    [Fact]
    public async Task Should_ReturnError_WhenEmailAlreadyExists()
    {
        // Arrange
        var repoMock = new Mock<IUserRepository>();

        repoMock
            .Setup(r => r.GetByEmailAsync("joao@test.com"))
            .ReturnsAsync(new UserEntity
            {
                Id = 1,
                Name = "Teste",
                Email = "joao@test.com",
                Password = "123456"
            });

        var useCase = new UserUseCase(repoMock.Object);

        var dto = new UserDto
        {
            Name = "Outro Usuário",
            Email = "joao@test.com",
            Password = "123456"
        };

        // Act
        var result = await useCase.AddUserAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.StatusCode);
        Assert.NotNull(result.Error);
        Assert.Equal("Unable to save user", result.Error);

    }
}
