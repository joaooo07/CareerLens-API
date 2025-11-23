using System.Net;
using CareerLens.Application.Dtos.Skills;
using CareerLens.Application.Interfaces.Skills;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Application.UseCases.Skills
{
    public class SkillUseCase : ISkillUseCase
    {
        private readonly ISkillRepository _repo;

        public SkillUseCase(ISkillRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<IEnumerable<SkillResponseDto>>> GetAllSkillsAsync()
        {
            var list = await _repo.GetAllAsync();

            // Lista vazia NÃO é erro
            var mapped = list.Select(s => s.ToResponse());

            return OperationResult<IEnumerable<SkillResponseDto>>.Success(mapped);
        }

        public async Task<OperationResult<SkillResponseDto?>> GetSkillByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity is null)
                return OperationResult<SkillResponseDto?>.Failure(
                    "Skill not found",
                    (int)HttpStatusCode.NotFound
                );

            return OperationResult<SkillResponseDto?>.Success(entity.ToResponse());
        }

        public async Task<OperationResult<SkillResponseDto?>> AddSkillAsync(SkillDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var created = await _repo.AddAsync(entity);

                return OperationResult<SkillResponseDto?>.Success(created.ToResponse());
            }
            catch
            {
                return OperationResult<SkillResponseDto?>.Failure(
                    "Unable to save skill",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }
    }
}
