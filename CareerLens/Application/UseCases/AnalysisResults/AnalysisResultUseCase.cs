using System.Net;
using CareerLens.Application.Dtos.AnalysisResults;
using CareerLens.Application.Interfaces.AnalysisResults;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Application.UseCases.AnalysisResults
{
    public class AnalysisResultUseCase : IAnalysisResultUseCase
    {
        private readonly IAnalysisResultRepository _repo;

        public AnalysisResultUseCase(IAnalysisResultRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<AnalysisResultResponseDto>> AddAsync(AnalysisResultDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var saved = await _repo.AddAsync(entity);

                return OperationResult<AnalysisResultResponseDto>
                    .Success(saved.ToResponse());
            }
            catch
            {
                return OperationResult<AnalysisResultResponseDto>
                    .Failure("Unable to save analysis result", StatusCodes.Status400BadRequest);
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);

                if (!deleted)
                {
                    return OperationResult<bool>
                        .Failure("Analysis result not found", StatusCodes.Status404NotFound);
                }

                return OperationResult<bool>.Success(true);
            }
            catch
            {
                return OperationResult<bool>
                    .Failure("Unable to delete analysis result", StatusCodes.Status400BadRequest);
            }
        }

        public async Task<OperationResult<AnalysisResultResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result is null)
            {
                return OperationResult<AnalysisResultResponseDto>
                    .Failure("Analysis result not found", StatusCodes.Status404NotFound);
            }

            return OperationResult<AnalysisResultResponseDto>
                .Success(result.ToResponse());
        }

        public async Task<OperationResult<IEnumerable<AnalysisResultResponseDto>>> GetByJobAnalysisIdAsync(int jobAnalysisId)
        {
            var result = await _repo.GetByJobAnalysisIdAsync(jobAnalysisId);

            if (!result.Any())
            {
                return OperationResult<IEnumerable<AnalysisResultResponseDto>>
                    .Failure("No analysis results found", StatusCodes.Status204NoContent);
            }

            var mapped = result.Select(x => x.ToResponse());

            return OperationResult<IEnumerable<AnalysisResultResponseDto>>
                .Success(mapped);
        }

        public async Task<OperationResult<AnalysisResultResponseDto>> UpdateAsync(int id, AnalysisResultDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var updated = await _repo.UpdateAsync(id, entity);

                if (updated is null)
                {
                    return OperationResult<AnalysisResultResponseDto>
                        .Failure("Analysis result not found", StatusCodes.Status404NotFound);
                }

                return OperationResult<AnalysisResultResponseDto>
                    .Success(updated.ToResponse());
            }
            catch
            {
                return OperationResult<AnalysisResultResponseDto>
                    .Failure("Unable to update analysis result", StatusCodes.Status400BadRequest);
            }
        }
    }
}
