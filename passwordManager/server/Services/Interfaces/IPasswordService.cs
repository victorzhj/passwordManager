using Microsoft.AspNetCore.Mvc;
using server.Dto;

namespace server.Services.Interfaces
{
    public interface IPasswordService
    {
        public Task<List<PasswordDto>> GetPasswords([FromBody] PasswordDto passwordDto);
        public Task<bool> AddPassword(PasswordDto passwordDto);
        public Task<bool> DeletePassword(PasswordDto passwordDto);
        public Task<bool> UpdatePassword(PasswordDto passwordDto);
    }
}
