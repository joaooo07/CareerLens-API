using CareerLens.Application.Dtos.Resumes;
using CareerLens.Application.Models;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Interfaces.Resumes
{
    public interface IResumeUseCase
    {
        Task<OperationResult<ResumeEntity?>> AddResumeAsync(ResumeDto dto);
        Task<OperationResult<ResumeEntity?>> UpdateResumeAsync(int id, ResumeDto dto);
        Task<OperationResult<bool>> DeleteResumeAsync(int id);
        Task<OperationResult<ResumeEntity?>> GetResumeByIdAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>> GetResumesByUserAsync(int userId, int page = 1, int pageSize = 10);
        Task<OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>> GetAllResumesAsync(int page = 1, int pageSize = 10);

    }
}
