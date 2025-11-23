using Xunit;
using Moq;
using CareerLens.Application.UseCases.ResumeSkills;
using CareerLens.Application.Dtos.ResumeSkills;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Test.ResumeSkills
{
    public class ResumeSkillUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnError_WhenResumeDoesNotExist()
        {
            // Arrange
            var repoMock = new Mock<IResumeSkillRepository>();
            var resumeRepoMock = new Mock<IResumeRepository>();
            var skillRepoMock = new Mock<ISkillRepository>();

            // Simula: Resume NÃO existe
            resumeRepoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ResumeEntity?)null);

            var useCase = new ResumeSkillUseCase(
                repoMock.Object,
                resumeRepoMock.Object,
                skillRepoMock.Object
            );

            var dto = new ResumeSkillDto
            {
                ResumeId = 1,
                SkillId = 99
            };

            // Act
            var result = await useCase.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Resume not found", result.Error);
            Assert.Null(result.Value);
        }
    }
}
