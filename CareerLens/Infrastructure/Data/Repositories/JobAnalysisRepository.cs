using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class JobAnalysisRepository : IJobAnalysisRepository
    {
        private readonly ApplicationDbContext _context;

        public JobAnalysisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobAnalysisEntity?> AddAsync(JobAnalysisEntity entity)
        {
            _context.JobAnalyses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.JobAnalyses.FindAsync(id);

            if (result is null)
                return false;

            _context.JobAnalyses.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JobAnalysisEntity?> UpdateAsync(int id, JobAnalysisEntity entity)
        {
            var result = await _context
                .JobAnalyses
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return null;

            result.JobTitle = entity.JobTitle;
            result.JobDescription = entity.JobDescription;
            result.UserId = entity.UserId;
            result.ResumeId = entity.ResumeId;
            result.CreatedAt = entity.CreatedAt;

            _context.JobAnalyses.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<PageResultModel<IEnumerable<JobAnalysisEntity>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.JobAnalyses.CountAsync();

            var data = await _context.JobAnalyses
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<JobAnalysisEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

        public async Task<JobAnalysisEntity?> GetByIdAsync(int id)
        {
            return await _context
                .JobAnalyses
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageResultModel<IEnumerable<JobAnalysisEntity>>> GetByUserAsync(int userId, int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.JobAnalyses
                .Where(x => x.UserId == userId);

            var totalItems = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<JobAnalysisEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

        public async Task<IEnumerable<JobAnalysisEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.JobAnalyses
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobAnalysisEntity>> GetByResumeIdAsync(int resumeId)
        {
            return await _context.JobAnalyses
                .Where(x => x.ResumeId == resumeId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobAnalysisEntity>> SearchByJobTitleAsync(string title)
        {
            return await _context.JobAnalyses
                .Where(x => x.JobTitle.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }
    }
}
