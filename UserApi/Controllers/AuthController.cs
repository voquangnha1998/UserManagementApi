using Microsoft.AspNetCore.Mvc;
using UserApi.DTOs;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        public AuthController(IUserService service) => _service = service;

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInRequestDto dto)
        {
            var token = await _service.SignInAsync(dto.Email, dto.Password);
            return token is null ? Unauthorized() : Ok(new { token });
        }
    }

}
