using Microsoft.AspNetCore.Mvc;
using server.Dto;
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

        [HttpGet("getPasswords")]
        public async Task<IActionResult> GetPasswords([FromBody] PasswordDto passwordDto)
        {
            try
            {
                var passwords = await passwordService.GetPasswords(passwordDto);
                if (passwords == null)
                {
                    return NotFound();
                }
                return Ok(passwords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addPassword")]
        public async Task<IActionResult> AddPassword([FromBody] PasswordDto passwordDto)
        {
            try
            {
                bool saved = await passwordService.AddPassword(passwordDto);
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

        [HttpDelete("deletePassword")]
        public async Task<IActionResult> DeletePassword([FromBody] PasswordDto passwordDto)
        {
            try
            {
                bool deleted = await passwordService.DeletePassword(passwordDto);
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

        [HttpPost("updatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordDto passwordDto)
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
