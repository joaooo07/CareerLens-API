using Xunit;
using Moq;
using CareerLens.Application.UseCases.LearningResources;
using CareerLens.Application.Dtos.LearningResources;
using CareerLens.Domain.Interfaces;
using CareerLens.Domain.Entities;

namespace CareerLens.Test.LearningResources
{
    public class LearningResourceUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnError_WhenSkillDoesNotExist_OnAdd()
        {
            // Arrange
            var repoMock = new Mock<ILearningResourceRepository>();
            var skillRepoMock = new Mock<ISkillRepository>();

            // Simula: SkillId foi informado, mas não existe
            skillRepoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((SkillEntity?)null);

            var useCase = new LearningResourceUseCase(repoMock.Object, skillRepoMock.Object);

            var dto = new LearningResourceDto
            {
                Title = "Curso de C#",
                Url = "http://curso.com",
                ResourceType = "Video",
                SkillId = 99 // skill inexistente
            };

            // Act
            var result = await useCase.AddResourceAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Skill not found", result.Error);
            Assert.Null(result.Value);
        }
    }
}
