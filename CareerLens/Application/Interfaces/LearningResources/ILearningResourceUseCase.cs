using CareerLens.Application.Dtos.LearningResources;
using CareerLens.Application.Models;

namespace CareerLens.Application.Interfaces.LearningResources
{
    public interface ILearningResourceUseCase
    {
        Task<OperationResult<IEnumerable<LearningResourceResponseDto>>> GetResourcesBySkillAsync(int skillId);
        Task<OperationResult<LearningResourceResponseDto?>> GetResourceByIdAsync(int id);
        Task<OperationResult<LearningResourceResponseDto?>> AddResourceAsync(LearningResourceDto dto);
        Task<OperationResult<LearningResourceResponseDto?>> UpdateResourceAsync(int id, LearningResourceDto dto);
        Task<OperationResult<bool>> DeleteResourceAsync(int id);
    }
}
