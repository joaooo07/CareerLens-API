using CareerLens.Application.Dtos.Resumes;
using CareerLens.Application.Interfaces.Resumes;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/resumes")]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeUseCase _useCase;

        public ResumeController(IResumeUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _useCase.GetAllResumesAsync(page, pageSize);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var response = new
            {
                data = result.Value!.Data,
                pagination = new
                {
                    result.Value.TotalItems,
                    result.Value.Page,
                    result.Value.PageSize
                },
                links = new
                {
                    self = Url.Action(nameof(GetAll)),
                    create = Url.Action(nameof(Create)),
                }
            };

            return Ok(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _useCase.GetResumeByIdAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var entity = result.Value;

            var response = new
            {
                data = entity,
                links = new
                {
                    self = Url.Action(nameof(GetById), new { id }),
                    update = Url.Action(nameof(Update), new { id }),
                    delete = Url.Action(nameof(Delete), new { id })
                }
            };

            return Ok(response);
        }


        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUser([FromRoute] int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _useCase.GetResumesByUserAsync(userId, page, pageSize);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return Ok(result.Value);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResumeDto dto)
        {
            var result = await _useCase.AddResumeAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ResumeDto dto)
        {
            var result = await _useCase.UpdateResumeAsync(id, dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return Ok(result.Value);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _useCase.DeleteResumeAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return NoContent();
        }
    }
}
