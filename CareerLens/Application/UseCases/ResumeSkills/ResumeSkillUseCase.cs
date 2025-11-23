using System.Net;
using CareerLens.Application.Dtos.ResumeSkills;
using CareerLens.Application.Interfaces.ResumeSkills;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Application.UseCases.ResumeSkills
{
    public class ResumeSkillUseCase : IResumeSkillUseCase
    {
        private readonly IResumeSkillRepository _repo;
        private readonly IResumeRepository _resumeRepo;
        private readonly ISkillRepository _skillRepo;

        public ResumeSkillUseCase(
            IResumeSkillRepository repo,
            IResumeRepository resumeRepo,
            ISkillRepository skillRepo)
        {
            _repo = repo;
            _resumeRepo = resumeRepo;
            _skillRepo = skillRepo;
        }

        public async Task<OperationResult<ResumeSkillResponseDto?>> AddAsync(ResumeSkillDto dto)
        {
            try
            {
                // Validar se o Resume existe
                var resume = await _resumeRepo.GetByIdAsync(dto.ResumeId);
                if (resume is null)
                    return OperationResult<ResumeSkillResponseDto?>.Failure(
                        "Resume not found",
                        (int)HttpStatusCode.BadRequest
                    );

                // Validar se o Skill existe
                var skill = await _skillRepo.GetByIdAsync(dto.SkillId);
                if (skill is null)
                    return OperationResult<ResumeSkillResponseDto?>.Failure(
                        "Skill not found",
                        (int)HttpStatusCode.BadRequest
                    );

                // Verificar se a relação já existe
                var exists = await _repo.ExistsAsync(dto.ResumeId, dto.SkillId);
                if (exists)
                    return OperationResult<ResumeSkillResponseDto?>.Failure(
                        "This skill is already linked to the resume",
                        (int)HttpStatusCode.Conflict
                    );

                // Criar entidade
                var entity = dto.ToEntity();
                var saved = await _repo.AddAsync(entity);

                return OperationResult<ResumeSkillResponseDto?>.Success(saved.ToResponse());
            }
            catch
            {
                return OperationResult<ResumeSkillResponseDto?>.Failure(
                    "Unable to add resume-skill relation",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(int resumeId, int skillId)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(resumeId, skillId);

                if (!deleted)
                    return OperationResult<bool>.Failure(
                        "Resume-skill relation not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<bool>.Success(true);
            }
            catch
            {
                return OperationResult<bool>.Failure(
                    "Unable to delete resume-skill relation",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }
    }
}
