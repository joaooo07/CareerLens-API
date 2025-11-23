using CareerLens.Application.Dtos.JobAnalyses;
using CareerLens.Application.Models;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Interfaces.JobAnalyses
{
    public interface IJobAnalysisUseCase
    {
        Task<OperationResult<JobAnalysisEntity?>> AddAsync(JobAnalysisDto dto);
        Task<OperationResult<JobAnalysisEntity?>> UpdateAsync(int id, JobAnalysisDto dto);
        Task<OperationResult<bool>> DeleteAsync(int id);
        Task<OperationResult<JobAnalysisEntity?>> GetByIdAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<JobAnalysisEntity>>>> GetByUserAsync(int userId, int page = 1, int pageSize = 10);
    }
}
