using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class LearningResourceRepository : ILearningResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public LearningResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LearningResourceEntity?> AddAsync(LearningResourceEntity entity)
        {
            _context.LearningResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.LearningResources.FindAsync(id);

            if (result is null)
                return false;

            _context.LearningResources.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LearningResourceEntity?> UpdateAsync(int id, LearningResourceEntity entity)
        {
            var result = await _context
                .LearningResources
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return null;

            result.Title = entity.Title;
            result.Url = entity.Url;
            result.ResourceType = entity.ResourceType;
            result.SkillId = entity.SkillId;

            _context.LearningResources.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<PageResultModel<IEnumerable<LearningResourceEntity>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.LearningResources.CountAsync();

            var data = await _context.LearningResources
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<LearningResourceEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

        public async Task<LearningResourceEntity?> GetByIdAsync(int id)
        {
            return await _context
                .LearningResources
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LearningResourceEntity>> GetBySkillIdAsync(int skillId)
        {
            return await _context.LearningResources
                .Where(x => x.SkillId == skillId)
                .ToListAsync();
        }

        public async Task<IEnumerable<LearningResourceEntity>> SearchByTitleAsync(string title)
        {
            return await _context.LearningResources
                .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }
    }
}
