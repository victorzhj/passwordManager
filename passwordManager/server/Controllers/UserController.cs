using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
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
        public IActionResult Register([FromBody] UserDto userDto)
        {
            try
            {
                var user = userService.Register(userDto);
                if (user == null)
                {
                    return BadRequest("Registration failed");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            try
            {
                var user = userService.Login(userDto);
                if (user == null)
                {
                    return BadRequest("Login failed");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("deleteUser")]
        public IActionResult Delete([FromBody] UserDto userDto)
        {
            try
            {
                var user = userService.Delete(userDto);
                if (user == null)
                {
                    return BadRequest("Deleting user failed");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    
}
