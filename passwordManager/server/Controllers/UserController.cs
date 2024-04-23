using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto.UserDtos;
using server.Services.Interfaces;
using System.Security.Claims;

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

        [HttpGet("getSalt")]
        public async Task<IActionResult> GetSalt([FromQuery] string username)
        {
            try
            {
                var salt = await userService.GetSalt(username);
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
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            try
            {   
                var userId = GetUserId();
                //userDeletationDto.UserId = userId;
                var deleted = await userService.Delete(userId);
                if (!deleted)
                {
                    return BadRequest("Deleting user failed");
                }
                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim.Value);
        }
    }
    
}
