using CareerLens.Application.Dtos.Skills;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class SkillMapper
    {
        // Entrada → Entity
        public static SkillEntity ToEntity(this SkillDto dto)
        {
            return new SkillEntity
            {
                Name = dto.Name,
                Category = dto.Category
            };
        }

        // Entity → Saída (DTO)
        public static SkillResponseDto ToResponse(this SkillEntity entity)
        {
            return new SkillResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Category = entity.Category
            };
        }
    }
}
