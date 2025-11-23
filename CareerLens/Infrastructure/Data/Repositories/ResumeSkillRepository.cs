using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class ResumeSkillRepository : IResumeSkillRepository
    {
        private readonly ApplicationDbContext _context;

        public ResumeSkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResumeSkillEntity?> AddAsync(ResumeSkillEntity entity)
        {
            _context.ResumeSkills.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int resumeId, int skillId)
        {
            var relation = await _context.ResumeSkills
                .FirstOrDefaultAsync(x =>
                    x.ResumeId == resumeId &&
                    x.SkillId == skillId);

            if (relation is null)
                return false;

            _context.ResumeSkills.Remove(relation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int resumeId, int skillId)
        {
            return await _context.ResumeSkills
                .AnyAsync(x =>
                    x.ResumeId == resumeId &&
                    x.SkillId == skillId);
        }
    }
}
