using DragerBackendTemplate.SOLID.DTOs;
using DragerBackendTemplate.SOLID.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DragerBackendTemplate.SOLID.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var token = await _userService.RegisterAsync(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var token = await _userService.LoginAsync(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                var token = authHeader.Replace("Bearer ", "");
                var user = await _userService.GetUserFromTokenAsync(token);
                return Ok(new { user.Email, user.Role }); // 👈 incluir rol
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users.Select(u => new { u.Id, u.Email }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var result = await _userService.DeleteUserAsync(email);
            if (result)
                return Ok(new { message = "Usuario eliminado" });
            else
                return NotFound(new { message = "Usuario no encontrado" });
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] DTOs.UpdateUserProfileRequest request)
        {
            var success = await _userService.UpdateUserProfileAsync(request);
            if (success)
                return Ok(new { message = "Perfil actualizado correctamente" });
            else
                return NotFound(new { message = "Usuario no encontrado o sin cambios" });
        }
    }
}