using CareerLens.Domain.Entities;
using CareerLens.Application.Models;

namespace CareerLens.Domain.Interfaces
{
    public interface IJobAnalysisRepository
    {
        Task<PageResultModel<IEnumerable<JobAnalysisEntity>>> GetByUserAsync(int userId, int page = 1, int pageSize = 10);
        Task<JobAnalysisEntity?> GetByIdAsync(int id);
        Task<JobAnalysisEntity?> AddAsync(JobAnalysisEntity entity);
        Task<JobAnalysisEntity?> UpdateAsync(int id, JobAnalysisEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
