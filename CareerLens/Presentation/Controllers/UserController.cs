using CareerLens.Application.Dtos.Users;
using CareerLens.Application.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace CareerLens.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _useCase;

        public UserController(IUserUseCase useCase)
        {
            _useCase = useCase;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _useCase.GetAllUsersAsync(page, pageSize);

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
            var result = await _useCase.GetUserByIdAsync(id);

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


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            var result = await _useCase.AddUserAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserDto dto)
        {
            var result = await _useCase.UpdateUserAsync(id, dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return Ok(result.Value);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _useCase.DeleteUserAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return NoContent();
        }
    }
}
