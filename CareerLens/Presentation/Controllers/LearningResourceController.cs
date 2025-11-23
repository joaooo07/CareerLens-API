using CareerLens.Application.Dtos.LearningResources;
using CareerLens.Application.Interfaces.LearningResources;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/learning-resources")]
    public class LearningResourceController : ControllerBase
    {
        private readonly ILearningResourceUseCase _useCase;

        public LearningResourceController(ILearningResourceUseCase useCase)
        {
            _useCase = useCase;
        }

        // ============================
        // GET BY ID
        // ============================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _useCase.GetResourceByIdAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var data = result.Value;

            var response = new
            {
                data,
                links = new
                {
                    self = Url.Action(nameof(GetById), new { id }),
                    update = Url.Action(nameof(Update), new { id }),
                    delete = Url.Action(nameof(Delete), new { id })
                }
            };

            return Ok(response);
        }

        // ============================
        // GET BY SKILL
        // ============================
        [HttpGet("skill/{skillId:int}")]
        public async Task<IActionResult> GetBySkill(int skillId)
        {
            var result = await _useCase.GetResourcesBySkillAsync(skillId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var data = result.Value;

            var response = new
            {
                data,
                links = new
                {
                    self = Url.Action(nameof(GetBySkill), new { skillId }),
                    create = Url.Action(nameof(Create))
                }
            };

            return Ok(response);
        }

        // ============================
        // CREATE
        // ============================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LearningResourceDto dto)
        {
            var result = await _useCase.AddResourceAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var id = result.Value!.Id;

            return CreatedAtAction(nameof(GetById), new { id }, result.Value);
        }

        // ============================
        // UPDATE
        // ============================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] LearningResourceDto dto)
        {
            var result = await _useCase.UpdateResourceAsync(id, dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return Ok(result.Value);
        }

        // ============================
        // DELETE
        // ============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.DeleteResourceAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return NoContent();
        }
    }
}
