using CareerLens.Application.Dtos.ResumeSkills;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class ResumeSkillMapper
    {
        public static ResumeSkillEntity ToEntity(this ResumeSkillDto dto)
        {
            return new ResumeSkillEntity
            {
                ResumeId = dto.ResumeId,
                SkillId = dto.SkillId
            };
        }

        public static ResumeSkillResponseDto ToResponse(this ResumeSkillEntity entity)
        {
            return new ResumeSkillResponseDto
            {
                ResumeId = entity.ResumeId ?? 0,
                SkillId = entity.SkillId ?? 0
            };
        }
    }
}
