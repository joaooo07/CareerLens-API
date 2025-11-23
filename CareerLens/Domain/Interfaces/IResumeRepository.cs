using CareerLens.Domain.Entities;
using CareerLens.Application.Models;

namespace CareerLens.Domain.Interfaces
{
    public interface IResumeRepository
    {
        Task<PageResultModel<IEnumerable<ResumeEntity>>> GetByUserAsync(int userId, int page = 1, int pageSize = 10);
        Task<ResumeEntity?> GetByIdAsync(int id);
        Task<ResumeEntity?> AddAsync(ResumeEntity entity);
        Task<ResumeEntity?> UpdateAsync(int id, ResumeEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<PageResultModel<IEnumerable<ResumeEntity>>> GetAllAsync(int page, int pageSize);
    }
}
