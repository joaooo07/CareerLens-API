using CareerLens.Application.Dtos.Resumes;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class ResumeMapper
    {
        public static ResumeEntity ToEntity(this ResumeDto dto)
        {
            return new ResumeEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId
            };
        }

        public static ResumeDto ToDto(this ResumeEntity entity)
        {
            return new ResumeDto
            {
                Title = entity.Title,
                Description = entity.Description,
                UserId = entity.UserId
            };
        }
    }
}
