using CareerLens.Application.Dtos.AnalysisResults;
using CareerLens.Application.Interfaces.AnalysisResults;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/analysis-results")]
    public class AnalysisResultController : ControllerBase
    {
        private readonly IAnalysisResultUseCase _useCase;

        public AnalysisResultController(IAnalysisResultUseCase useCase)
        {
            _useCase = useCase;
        }

        // GET /id
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
                    update = Url.Action(nameof(Update), new { id }),
                    delete = Url.Action(nameof(Delete), new { id })
                }
            };

            return Ok(response);
        }

        // GET /job-analysis/{jobAnalysisId}
        [HttpGet("job-analysis/{jobAnalysisId:int}")]
        public async Task<IActionResult> GetByJobAnalysisId(int jobAnalysisId)
        {
            var result = await _useCase.GetByJobAnalysisIdAsync(jobAnalysisId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            var response = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetByJobAnalysisId), new { jobAnalysisId }),
                    create = Url.Action(nameof(Create))
                }
            };

            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnalysisResultDto dto)
        {
            var result = await _useCase.AddAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnalysisResultDto dto)
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
