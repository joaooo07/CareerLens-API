using System.Net;
using CareerLens.Application.Dtos.Resumes;
using CareerLens.Application.Interfaces.Resumes;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Application.UseCases.Resumes
{
    public class ResumeUseCase : IResumeUseCase
    {
        private readonly IResumeRepository _repo;

        public ResumeUseCase(IResumeRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<ResumeEntity?>> AddResumeAsync(ResumeDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var result = await _repo.AddAsync(entity);

                return OperationResult<ResumeEntity?>.Success(result);
            }
            catch
            {
                return OperationResult<ResumeEntity?>.Failure(
                    "Unable to save resume",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<ResumeEntity?>> UpdateResumeAsync(int id, ResumeDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var result = await _repo.UpdateAsync(id, entity);

                if (result is null)
                    return OperationResult<ResumeEntity?>.Failure(
                        "Resume not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<ResumeEntity?>.Success(result);
            }
            catch
            {
                return OperationResult<ResumeEntity?>.Failure(
                    "Unable to update resume",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<bool>> DeleteResumeAsync(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);

                if (!deleted)
                    return OperationResult<bool>.Failure(
                        "Resume not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<bool>.Success(true);
            }
            catch
            {
                return OperationResult<bool>.Failure(
                    "Unable to delete resume",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<ResumeEntity?>> GetResumeByIdAsync(int id)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result is null)
                return OperationResult<ResumeEntity?>.Failure(
                    "Resume not found",
                    (int)HttpStatusCode.NotFound
                );

            return OperationResult<ResumeEntity?>.Success(result);
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>> GetResumesByUserAsync(int userId, int page = 1, int pageSize = 10)
        {
            var result = await _repo.GetByUserAsync(userId, page, pageSize);

            return OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>.Success(result);
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>> GetAllResumesAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                var result = await _repo.GetAllAsync(page, pageSize);
                return OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>.Success(result);
            }
            catch
            {
                return OperationResult<PageResultModel<IEnumerable<ResumeEntity>>>.Failure(
                    "Failed to retrieve resumes.",
                    StatusCodes.Status500InternalServerError
                );
            }
        }


    }
}
