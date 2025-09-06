using Microsoft.AspNetCore.Mvc;
using UserApi.DTOs;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (users, total) = await _service.SearchAsync(search, page, pageSize);
            return Ok(new { items = users, page, pageSize, total });
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto dto)
        {
            var (success, error) = await _service.CreateAsync(dto);
            return success ? StatusCode(201) : Conflict(new { error });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserUpdateDto dto)
        {
            var (success, error) = await _service.UpdateAsync(id, dto);
            return success ? Ok() : Conflict(new { error });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }

}
