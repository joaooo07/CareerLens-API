using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class AnalysisResultRepository : IAnalysisResultRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalysisResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnalysisResultEntity?> AddAsync(AnalysisResultEntity entity)
        {
            _context.AnalysisResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.AnalysisResults.FindAsync(id);

            if (result is null)
                return false;

            _context.AnalysisResults.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AnalysisResultEntity?> UpdateAsync(int id, AnalysisResultEntity entity)
        {
            var result = await _context.AnalysisResults
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return null;

            result.Status = entity.Status;
            result.SkillId = entity.SkillId;
            result.JobAnalysisId = entity.JobAnalysisId;
            // NÃO atualizar CreatedAt

            _context.AnalysisResults.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<PageResultModel<IEnumerable<AnalysisResultEntity>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.AnalysisResults.CountAsync();

            var data = await _context.AnalysisResults
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<AnalysisResultEntity>>(
                data, totalItems, page, pageSize
            );
        }

        public async Task<AnalysisResultEntity?> GetByIdAsync(int id)
        {
            return await _context.AnalysisResults
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AnalysisResultEntity>> GetByJobAnalysisIdAsync(int jobAnalysisId)
        {
            return await _context.AnalysisResults
                .Where(x => x.JobAnalysisId == jobAnalysisId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnalysisResultEntity>> GetBySkillIdAsync(int skillId)
        {
            return await _context.AnalysisResults
                .Where(x => x.SkillId == skillId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnalysisResultEntity>> GetByStatusAsync(string status)
        {
            return await _context.AnalysisResults
                .Where(x => x.Status == status)
                .ToListAsync();
        }
    }
}
