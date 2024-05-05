using server.Models;
using server.Dto.UserDtos;
using System.Security.Claims;

namespace server.Services.Interfaces
{
    /// <summary>
    /// Represents the interface for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user with the provided user creation data.
        /// </summary>
        /// <param name="userCreationDto">The user creation data.</param>
        /// <returns>The username of the registered user.</returns>
        public Task<UsernameDto> Register(UserCreationDto userCreationDto);

        /// <summary>
        /// Retrieves the salt value associated with the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The salt value of the user.</returns>
        public Task<UserSaltDto> GetSalt(string username);

        /// <summary>
        /// Authenticates a user with the provided login data.
        /// </summary>
        /// <param name="userLoginDto">The user login data.</param>
        /// <returns>The authentication token for the user.</returns>
        public Task<TokenDto> Login(UserLoginDto userLoginDto);

        /// <summary>
        /// Deletes a user with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>A boolean indicating whether the user was successfully deleted.</returns>
        public Task<bool> Delete(int userId);
    }
}