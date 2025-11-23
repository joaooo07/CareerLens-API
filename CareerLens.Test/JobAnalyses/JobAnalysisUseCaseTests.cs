using Xunit;
using Moq;
using CareerLens.Application.UseCases.JobAnalyses;
using CareerLens.Application.Dtos.JobAnalyses;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Test.JobAnalyses
{
    public class JobAnalysisUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnError_WhenRepositoryThrowsException_OnAdd()
        {
            // Arrange
            var repoMock = new Mock<IJobAnalysisRepository>();

            // Simula erro no repositório (ex: falha de banco)
            repoMock
                .Setup(r => r.AddAsync(It.IsAny<JobAnalysisEntity>()))
                .ThrowsAsync(new Exception("DB error"));

            var useCase = new JobAnalysisUseCase(repoMock.Object);

            var dto = new JobAnalysisDto
            {
                JobTitle = "Backend Developer",
                JobDescription = "Vaga para .NET",
                UserId = 1,
                ResumeId = 1
            };

            // Act
            var result = await useCase.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Unable to save job analysis", result.Error);
            Assert.Null(result.Value);
        }
    }
}
