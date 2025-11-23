using Xunit;
using Moq;
using CareerLens.Application.UseCases.Skills;
using CareerLens.Application.Dtos.Skills;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Test.Skills
{
    public class SkillUseCaseTests
    {
        [Fact]
        public async Task Should_CreateSkill_WhenValid()
        {
            // Arrange
            var repoMock = new Mock<ISkillRepository>();

            repoMock
                .Setup(r => r.AddAsync(It.IsAny<SkillEntity>()))
                .ReturnsAsync(new SkillEntity
                {
                    Id = 10,
                    Name = "C#",
                    Category = "ProgrammingLanguage"
                });

            var useCase = new SkillUseCase(repoMock.Object);

            var dto = new SkillDto
            {
                Name = "C#",
                Category = "ProgrammingLanguage"
            };

            // Act
            var result = await useCase.AddSkillAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.StatusCode);

            Assert.NotNull(result.Value);
            Assert.Equal(10, result.Value!.Id);
            Assert.Equal("C#", result.Value.Name);
            Assert.Equal("ProgrammingLanguage", result.Value.Category);
        }
    }
}
