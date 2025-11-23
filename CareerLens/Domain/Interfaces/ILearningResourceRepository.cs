using CareerLens.Domain.Entities;

namespace CareerLens.Domain.Interfaces
{
    public interface ILearningResourceRepository
    {
        Task<IEnumerable<LearningResourceEntity>> GetBySkillIdAsync(int skillId);
        Task<LearningResourceEntity?> GetByIdAsync(int id);
        Task<LearningResourceEntity?> AddAsync(LearningResourceEntity entity);
        Task<LearningResourceEntity?> UpdateAsync(int id, LearningResourceEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
