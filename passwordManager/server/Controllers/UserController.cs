﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto.UserDtos;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService) 
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreationDto userCreationDto)
        {
            try
            {
                var user = await userService.Register(userCreationDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("getSalt")]
        public async Task<IActionResult> GetSalt([FromBody] UsernameDto usernameDto)
        {
            try
            {
                var salt = await userService.GetSalt(usernameDto);
                return Ok(salt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var user = await userService.Login(userLoginDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] UserDeletationDto userDeletationDto)
        {
            try
            {
                var deleted = await userService.Delete(userDeletationDto);
                if (!deleted)
                {
                    return BadRequest("Deleting user failed");
                }
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    
}
