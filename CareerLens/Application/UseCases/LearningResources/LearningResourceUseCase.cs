using System.Net;
using CareerLens.Application.Dtos.LearningResources;
using CareerLens.Application.Interfaces.LearningResources;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Interfaces;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.UseCases.LearningResources
{
    public class LearningResourceUseCase : ILearningResourceUseCase
    {
        private readonly ILearningResourceRepository _repo;
        private readonly ISkillRepository _skillRepo;

        public LearningResourceUseCase(ILearningResourceRepository repo, ISkillRepository skillRepo)
        {
            _repo = repo;
            _skillRepo = skillRepo;
        }

        public async Task<OperationResult<IEnumerable<LearningResourceResponseDto>>> GetResourcesBySkillAsync(int skillId)
        {
            var resources = await _repo.GetBySkillIdAsync(skillId);

            if (!resources.Any())
                return OperationResult<IEnumerable<LearningResourceResponseDto>>.Failure(
                    "No learning resources found for this skill",
                    (int)HttpStatusCode.NoContent
                );

            var response = resources.Select(r => r.ToResponse());
            return OperationResult<IEnumerable<LearningResourceResponseDto>>.Success(response);
        }

        public async Task<OperationResult<LearningResourceResponseDto?>> GetResourceByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity is null)
                return OperationResult<LearningResourceResponseDto?>.Failure(
                    "Learning resource not found",
                    (int)HttpStatusCode.NotFound
                );

            return OperationResult<LearningResourceResponseDto?>.Success(entity.ToResponse());
        }

        public async Task<OperationResult<LearningResourceResponseDto?>> AddResourceAsync(LearningResourceDto dto)
        {
            try
            {
                // SkillId opcional, MAS se vier, precisa existir
                if (dto.SkillId is not null)
                {
                    var skillExists = await _skillRepo.GetByIdAsync(dto.SkillId.Value);
                    if (skillExists is null)
                        return OperationResult<LearningResourceResponseDto?>.Failure(
                            "Skill not found",
                            (int)HttpStatusCode.BadRequest
                        );
                }

                var entity = dto.ToEntity();
                var saved = await _repo.AddAsync(entity);

                return OperationResult<LearningResourceResponseDto?>.Success(saved.ToResponse());
            }
            catch
            {
                return OperationResult<LearningResourceResponseDto?>.Failure(
                    "Unable to save learning resource",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<LearningResourceResponseDto?>> UpdateResourceAsync(int id, LearningResourceDto dto)
        {
            try
            {
                // SkillId opcional, MAS se vier, precisa existir
                if (dto.SkillId is not null)
                {
                    var skillExists = await _skillRepo.GetByIdAsync(dto.SkillId.Value);
                    if (skillExists is null)
                        return OperationResult<LearningResourceResponseDto?>.Failure(
                            "Skill not found",
                            (int)HttpStatusCode.BadRequest
                        );
                }

                var entity = dto.ToEntity();
                var updated = await _repo.UpdateAsync(id, entity);

                if (updated is null)
                    return OperationResult<LearningResourceResponseDto?>.Failure(
                        "Learning resource not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<LearningResourceResponseDto?>.Success(updated.ToResponse());
            }
            catch
            {
                return OperationResult<LearningResourceResponseDto?>.Failure(
                    "Unable to update learning resource",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<bool>> DeleteResourceAsync(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);

                if (!deleted)
                    return OperationResult<bool>.Failure(
                        "Learning resource not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<bool>.Success(true);
            }
            catch
            {
                return OperationResult<bool>.Failure(
                    "Unable to delete learning resource",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }
    }
}
