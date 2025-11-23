using CareerLens.Application.Dtos.JobAnalyses;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class JobAnalysisMapper
    {
        // Entrada → Entity
        public static JobAnalysisEntity ToEntity(this JobAnalysisDto dto)
        {
            return new JobAnalysisEntity
            {
                JobTitle = dto.JobTitle,
                JobDescription = dto.JobDescription,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId,
                ResumeId = dto.ResumeId
            };
        }

        // Entity → Saída
        public static JobAnalysisResponseDto ToResponse(this JobAnalysisEntity entity)
        {
            return new JobAnalysisResponseDto
            {
                Id = entity.Id,
                JobTitle = entity.JobTitle,
                JobDescription = entity.JobDescription,
                CreatedAt = entity.CreatedAt,
                UserId = entity.UserId,
                ResumeId = entity.ResumeId
            };
        }
    }
}
