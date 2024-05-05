using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto.UserDtos;
using server.Services.Interfaces;
using System.Security.Claims;

namespace server.Controllers
{
    /// <summary>
    /// Controller class for handling user-related API endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService) 
        {
            this.userService = userService;
        }
        
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userCreationDto">The user creation data transfer object.</param>
        /// <returns>The created user.</returns>
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

        /// <summary>
        /// Retrieves the salt for a given username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The salt value.</returns>
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

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="userLoginDto">The user login data transfer object.</param>
        /// <returns>The authenticated user.</returns>
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

        /// <summary>
        /// Deletes the authenticated user.
        /// </summary>
        /// <returns>A response indicating the result of the deletion.</returns>
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

        /// <summary>
        /// Gets the user ID from the claims of the authenticated user.
        /// </summary>
        /// <returns>The user ID.</returns>
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim.Value);
        }
    }
}
