using CareerLens.Application.Dtos.AnalysisResults;
using CareerLens.Application.Models;

namespace CareerLens.Application.Interfaces.AnalysisResults
{
    public interface IAnalysisResultUseCase
    {
        Task<OperationResult<IEnumerable<AnalysisResultResponseDto>>> GetByJobAnalysisIdAsync(int jobAnalysisId);
        Task<OperationResult<AnalysisResultResponseDto>> GetByIdAsync(int id);
        Task<OperationResult<AnalysisResultResponseDto>> AddAsync(AnalysisResultDto dto);
        Task<OperationResult<AnalysisResultResponseDto>> UpdateAsync(int id, AnalysisResultDto dto);
        Task<OperationResult<bool>> DeleteAsync(int id);
    }
}
