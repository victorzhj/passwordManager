using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;
using server.Models;

namespace server.Services.Interfaces
{
    /// <summary>
    /// Represents the interface for managing passwords.
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Retrieves a list of password details for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of password details.</returns>
        public Task<List<PasswordDetailsDto>> GetPasswords(int userId);

        /// <summary>
        /// Adds a new password using the provided password add DTO.
        /// </summary>
        /// <param name="passwordAddDto">The DTO containing the password details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the password was added successfully.</returns>
        public Task<bool> AddPassword(PasswordAddDto passwordAddDto);

        /// <summary>
        /// Adds a new password using the provided password object.
        /// </summary>
        /// <param name="password">The password object containing the password details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the password was added successfully.</returns>
        public Task<bool> AddPassword(Password password);

        /// <summary>
        /// Deletes a password for a given user.
        /// </summary>
        /// <param name="passwordIdDto">The ID of the password to delete.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the password was deleted successfully.</returns>
        public Task<bool> DeletePassword(string passwordIdDto, int userId);

        /// <summary>
        /// Updates an existing password using the provided password add DTO.
        /// </summary>
        /// <param name="passwordDto">The DTO containing the updated password details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the password was updated successfully.</returns>
        public Task<bool> UpdatePassword(PasswordAddDto passwordDto);
    }
}
