using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IActionResult GetPasswords([FromBody] PasswordDto passwordDto)
        {
            try
            {
                var passwords = passwordService.GetPasswords(passwordDto);
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
        public IActionResult AddPassword([FromBody] PasswordDto passwordDto)
        {
            try
            {
                bool saved = passwordService.AddPassword(passwordDto);
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
        public IActionResult DeletePassword([FromBody] PasswordDto passwordDto)
        {
            try
            {
                bool deleted = passwordService.DeletePassword(passwordDto);
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
        public IActionResult UpdatePassword([FromBody] PasswordDto passwordDto)
        {
            try
            {
                bool saved = passwordService.updatePassword(passwordDto);
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
