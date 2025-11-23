using CareerLens.Application.Dtos.AnalysisResults;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class AnalysisResultMapper
    {
        // DTO de entrada -> Entity
        public static AnalysisResultEntity ToEntity(this AnalysisResultDto dto)
        {
            return new AnalysisResultEntity
            {
                Status = dto.Status,
                SkillId = dto.SkillId,
                JobAnalysisId = dto.JobAnalysisId
                // CreatedAt é gerado pela entidade (DateTime.UtcNow)
            };
        }

        // Entity -> DTO de saída
        public static AnalysisResultResponseDto ToResponse(this AnalysisResultEntity entity)
        {
            return new AnalysisResultResponseDto(
                entity.Id,
                entity.Status,
                entity.CreatedAt,
                entity.SkillId,
                entity.JobAnalysisId
            );
        }
    }
}
