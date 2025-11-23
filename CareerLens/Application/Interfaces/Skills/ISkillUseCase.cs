using CareerLens.Application.Dtos.Skills;
using CareerLens.Application.Models;

namespace CareerLens.Application.Interfaces.Skills
{
    public interface ISkillUseCase
    {
        Task<OperationResult<IEnumerable<SkillResponseDto>>> GetAllSkillsAsync();
        Task<OperationResult<SkillResponseDto?>> GetSkillByIdAsync(int id);
        Task<OperationResult<SkillResponseDto?>> AddSkillAsync(SkillDto dto);
    }
}
