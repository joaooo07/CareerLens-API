using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SkillEntity?> AddAsync(SkillEntity entity)
        {
            _context.Skills.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Skills.FindAsync(id);

            if (result is null)
                return false;

            _context.Skills.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SkillEntity?> UpdateAsync(int id, SkillEntity entity)
        {
            var result = await _context
                .Skills
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return null;

            result.Name = entity.Name;
            result.Category = entity.Category;

            _context.Skills.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }
        public async Task<IEnumerable<SkillEntity>> GetAllAsync()
        {
            return await _context.Skills
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        public async Task<PageResultModel<IEnumerable<SkillEntity>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.Skills.CountAsync();

            var data = await _context.Skills
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<SkillEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

        public async Task<SkillEntity?> GetByIdAsync(int id)
        {
            return await _context
                .Skills
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SkillEntity>> SearchByNameAsync(string name)
        {
            return await _context.Skills
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<SkillEntity>> GetByCategoryAsync(string category)
        {
            return await _context.Skills
                .Where(x => x.Category == category)
                .ToListAsync();
        }
    }
}
