using CareerLens.Application.Dtos.LearningResources;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class LearningResourceMapper
    {
        public static LearningResourceEntity ToEntity(this LearningResourceDto dto)
        {
            return new LearningResourceEntity
            {
                Title = dto.Title,
                Url = dto.Url,
                ResourceType = dto.ResourceType,
                SkillId = dto.SkillId
            };
        }

        public static LearningResourceResponseDto ToResponse(this LearningResourceEntity entity)
        {
            return new LearningResourceResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Url = entity.Url,
                ResourceType = entity.ResourceType,
                SkillId = entity.SkillId
            };
        }
    }
}
