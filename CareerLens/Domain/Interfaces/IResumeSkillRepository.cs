using CareerLens.Domain.Entities;

namespace CareerLens.Domain.Interfaces
{
    public interface IResumeSkillRepository
    {
        Task<ResumeSkillEntity?> AddAsync(ResumeSkillEntity entity);
        Task<bool> DeleteAsync(int resumeId, int skillId);
        Task<bool> ExistsAsync(int resumeId, int skillId);
    }
}
