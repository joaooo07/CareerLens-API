using CareerLens.Application.Dtos.Skills;
using CareerLens.Application.Interfaces.Skills;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/skills")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillUseCase _useCase;

        public SkillController(ISkillUseCase useCase)
        {
            _useCase = useCase;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _useCase.GetAllSkillsAsync();

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var response = new
            {
                data = result.Value!,
                links = new
                {
                    self = Url.Action(nameof(GetAll)),
                    create = Url.Action(nameof(Create))
                }
            };

            return Ok(response);
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _useCase.GetSkillByIdAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var response = new
            {
                data = result.Value!,
                links = new
                {
                    self = Url.Action(nameof(GetById), new { id }),
                    delete = Url.Action(nameof(Delete), new { id })
                }
            };

            return Ok(response);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkillDto dto)
        {
            var result = await _useCase.AddSkillAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        // DELETE (opcional — depende de você)
        // Se quiser implementar, basta me pedir que fazemos a parte do repositório também.
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(501, "Delete not implemented for Skills");
        }
    }
}
