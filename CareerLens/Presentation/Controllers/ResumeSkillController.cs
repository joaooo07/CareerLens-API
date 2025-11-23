using CareerLens.Application.Dtos.ResumeSkills;
using CareerLens.Application.Interfaces.ResumeSkills;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/resume-skills")]
    public class ResumeSkillController : ControllerBase
    {
        private readonly IResumeSkillUseCase _useCase;

        public ResumeSkillController(IResumeSkillUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ResumeSkillDto dto)
        {
            var result = await _useCase.AddAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var response = new
            {
                data = result.Value,
                links = new
                {
                    delete = Url.Action(nameof(Delete), new { resumeId = dto.ResumeId, skillId = dto.SkillId })
                }
            };

            return Created(string.Empty, response);
        }

        [HttpDelete("{resumeId:int}/{skillId:int}")]
        public async Task<IActionResult> Delete(int resumeId, int skillId)
        {
            var result = await _useCase.DeleteAsync(resumeId, skillId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return NoContent();
        }
    }
}
