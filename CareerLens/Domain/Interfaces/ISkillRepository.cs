using CareerLens.Domain.Entities;

namespace CareerLens.Domain.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<SkillEntity>> GetAllAsync();
        Task<SkillEntity?> GetByIdAsync(int id);
        Task<SkillEntity?> AddAsync(SkillEntity entity);
    }
}
