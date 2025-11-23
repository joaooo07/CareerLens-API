using CareerLens.Application.Dtos.JobAnalyses;
using CareerLens.Application.Interfaces.JobAnalyses;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/job-analyses")]
    public class JobAnalysisController : ControllerBase
    {
        private readonly IJobAnalysisUseCase _useCase;

        public JobAnalysisController(IJobAnalysisUseCase useCase)
        {
            _useCase = useCase;
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _useCase.GetByIdAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var response = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), new { id }),
                    delete = Url.Action(nameof(Delete), new { id }),
                    update = Url.Action(nameof(Update), new { id })
                }
            };

            return Ok(response);
        }

        // GET BY USER (Paginated)
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUser(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _useCase.GetByUserAsync(userId, page, pageSize);

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
                    self = Url.Action(nameof(GetByUser), new { userId, page, pageSize }),
                    create = Url.Action(nameof(Create))
                }
            };

            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobAnalysisDto dto)
        {
            var result = await _useCase.AddAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobAnalysisDto dto)
        {
            var result = await _useCase.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return Ok(result.Value);
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.DeleteAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return NoContent();
        }
    }
}
