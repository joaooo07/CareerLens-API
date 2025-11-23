using CareerLens.Domain.Entities;

namespace CareerLens.Domain.Interfaces
{
    public interface IAnalysisResultRepository
    {
        Task<IEnumerable<AnalysisResultEntity>> GetByJobAnalysisIdAsync(int jobAnalysisId);
        Task<AnalysisResultEntity?> GetByIdAsync(int id);
        Task<AnalysisResultEntity?> AddAsync(AnalysisResultEntity entity);
        Task<AnalysisResultEntity?> UpdateAsync(int id, AnalysisResultEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
