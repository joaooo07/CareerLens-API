using Xunit;
using Moq;
using CareerLens.Application.UseCases.AnalysisResults;
using CareerLens.Application.Dtos.AnalysisResults;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Test.AnalysisResults
{
    public class AnalysisResultUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnError_WhenRepositoryThrowsException_OnAdd()
        {
            // Arrange
            var repoMock = new Mock<IAnalysisResultRepository>();

            // Simula falha no repositório (ex: erro de banco)
            repoMock
                .Setup(r => r.AddAsync(It.IsAny<AnalysisResultEntity>()))
                .ThrowsAsync(new Exception("DB error"));

            var useCase = new AnalysisResultUseCase(repoMock.Object);

            var dto = new AnalysisResultDto
            {
                Status = "Match",
                SkillId = 1,
                JobAnalysisId = 1
            };

            // Act
            var result = await useCase.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Unable to save analysis result", result.Error);
            Assert.Null(result.Value);
        }
    }
}
