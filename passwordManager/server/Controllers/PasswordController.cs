using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;
using server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace server.Controllers
{
    /// <summary>
    /// Controller class for managing passwords.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService passwordService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordController"/> class.
        /// </summary>
        /// <param name="passwordService">The password service.</param>
        public PasswordController(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        /// <summary>
        /// Gets the passwords for the current user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the response.</returns>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetPasswords()
        {
            try
            {
                var passwords = await passwordService.GetPasswords(GetUserId());
                return Ok(passwords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new password for the current user.
        /// </summary>
        /// <param name="passwordAddDto">The password to add.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response.</returns>
        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> AddPassword([FromBody] PasswordAddDto passwordAddDto)
        {
            try
            {
                passwordAddDto.UserId = GetUserId();
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

        /// <summary>
        /// Deletes a password for the current user.
        /// </summary>
        /// <param name="passwordId">The ID of the password to delete.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response.</returns>
        [HttpDelete()]
        [Authorize]
        public async Task<IActionResult> DeletePassword([FromQuery] string passwordId)
        {
            try
            {
                bool deleted = await passwordService.DeletePassword(passwordId, GetUserId());
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
        /// <summary>
        /// Updates a password for the current user.
        /// </summary>
        /// <param name="passwordDto">The updated password.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response.</returns>
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

        // Get the user ID from the token
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim.Value);
        }
    }
}
