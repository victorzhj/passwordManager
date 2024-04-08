﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet()]
        public async Task<IActionResult> GetPasswords([FromBody] PasswordAddDto passwordDto)
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

        [HttpPost()]
        public async Task<IActionResult> AddPassword([FromBody] PasswordAddDto passwordDto)
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

        [HttpDelete()]
        public async Task<IActionResult> DeletePassword([FromBody] PasswordAddDto passwordDto)
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

        [HttpPost("update")]
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
