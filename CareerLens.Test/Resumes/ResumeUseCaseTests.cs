using Xunit;
using Moq;
using CareerLens.Application.UseCases.Resumes;
using CareerLens.Application.Dtos.Resumes;
using CareerLens.Domain.Interfaces;
using CareerLens.Domain.Entities;

namespace CareerLens.Test.Resumes
{
    public class ResumeUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            var repoMock = new Mock<IResumeRepository>();

            // Simula erro no banco ao tentar salvar
            repoMock
                .Setup(r => r.AddAsync(It.IsAny<ResumeEntity>()))
                .ThrowsAsync(new Exception("DB error"));

            var useCase = new ResumeUseCase(repoMock.Object);

            var dto = new ResumeDto
            {
                Title = "Backend Developer",
                Description = "Experiência com .NET",
                UserId = 1
            };

            // Act
            var result = await useCase.AddResumeAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Unable to save resume", result.Error);
            Assert.Null(result.Value);
        }
    }
}
