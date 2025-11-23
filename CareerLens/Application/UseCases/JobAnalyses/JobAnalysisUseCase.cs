using System.Net;
using CareerLens.Application.Dtos.JobAnalyses;
using CareerLens.Application.Interfaces.JobAnalyses;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Application.UseCases.JobAnalyses
{
    public class JobAnalysisUseCase : IJobAnalysisUseCase
    {
        private readonly IJobAnalysisRepository _repo;

        public JobAnalysisUseCase(IJobAnalysisRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<JobAnalysisEntity?>> AddAsync(JobAnalysisDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var result = await _repo.AddAsync(entity);

                return OperationResult<JobAnalysisEntity?>.Success(result);
            }
            catch
            {
                return OperationResult<JobAnalysisEntity?>.Failure(
                    "Unable to save job analysis",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<JobAnalysisEntity?>> UpdateAsync(int id, JobAnalysisDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var result = await _repo.UpdateAsync(id, entity);

                if (result is null)
                    return OperationResult<JobAnalysisEntity?>.Failure(
                        "Job analysis not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<JobAnalysisEntity?>.Success(result);
            }
            catch
            {
                return OperationResult<JobAnalysisEntity?>.Failure(
                    "Unable to update job analysis",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);

                if (!deleted)
                    return OperationResult<bool>.Failure(
                        "Job analysis not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<bool>.Success(true);
            }
            catch
            {
                return OperationResult<bool>.Failure(
                    "Unable to delete job analysis",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<JobAnalysisEntity?>> GetByIdAsync(int id)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result is null)
                return OperationResult<JobAnalysisEntity?>.Failure(
                    "Job analysis not found",
                    (int)HttpStatusCode.NotFound
                );

            return OperationResult<JobAnalysisEntity?>.Success(result);
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<JobAnalysisEntity>>>> GetByUserAsync(int userId, int page = 1, int pageSize = 10)
        {
            var result = await _repo.GetByUserAsync(userId, page, pageSize);

            if (result.Data is null || !result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<JobAnalysisEntity>>>.Failure(
                    "No job analyses found for this user",
                    (int)HttpStatusCode.NoContent
                );

            return OperationResult<PageResultModel<IEnumerable<JobAnalysisEntity>>>.Success(result);
        }
    }
}
