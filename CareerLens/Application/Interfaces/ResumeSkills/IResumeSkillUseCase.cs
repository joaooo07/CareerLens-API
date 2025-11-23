using CareerLens.Application.Dtos.ResumeSkills;
using CareerLens.Application.Models;

namespace CareerLens.Application.Interfaces.ResumeSkills
{
    public interface IResumeSkillUseCase
    {
        Task<OperationResult<ResumeSkillResponseDto?>> AddAsync(ResumeSkillDto dto);
        Task<OperationResult<bool>> DeleteAsync(int resumeId, int skillId);
    }
}
