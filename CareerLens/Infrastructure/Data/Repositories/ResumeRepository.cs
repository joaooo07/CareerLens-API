using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly ApplicationDbContext _context;

        public ResumeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResumeEntity?> AddAsync(ResumeEntity entity)
        {
            _context.Resumes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Resumes.FindAsync(id);

            if (result is null)
                return false;

            _context.Resumes.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ResumeEntity?> UpdateAsync(int id, ResumeEntity entity)
        {
            var result = await _context
                .Resumes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return null;

            result.Title = entity.Title;
            result.Description = entity.Description;
            result.UserId = entity.UserId;

            _context.Resumes.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<PageResultModel<IEnumerable<ResumeEntity>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.Resumes.CountAsync();

            var data = await _context.Resumes
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ResumeEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

        public async Task<ResumeEntity?> GetByIdAsync(int id)
        {
            return await _context
                .Resumes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageResultModel<IEnumerable<ResumeEntity>>> GetByUserAsync(int userId, int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Resumes
                .Where(x => x.UserId == userId);

            var totalItems = await query.CountAsync();

            var data = await query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ResumeEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

    }
}
