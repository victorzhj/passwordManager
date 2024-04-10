using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService passwordService;
        public PasswordController(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetPasswords([FromQuery] int userId)
        {
            try
            {
                var passwords = await passwordService.GetPasswords(userId);
                return Ok(passwords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> AddPassword([FromBody] PasswordAddDto passwordAddDto)
        {
            try
            {
                bool saved = await passwordService.AddPassword(passwordAddDto);
                if (!saved)
                {
                    return BadRequest("Cannot add password");
                }
                return Ok("Password saved");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete()]
        [Authorize]
        public async Task<IActionResult> DeletePassword([FromBody] PasswordIdDto passwordIdDto)
        {
            try
            {
                bool deleted = await passwordService.DeletePassword(passwordIdDto);
                if (!deleted)
                {
                    return BadRequest("Cannot delete password.");
                }
                return Ok("Password deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // TODO
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordAddDto passwordDto)
        {
            try
            {
                bool saved = await passwordService.UpdatePassword(passwordDto);
                if (!saved)
                {
                    return BadRequest("Cannot update password");
                }
                return Ok("Password updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
