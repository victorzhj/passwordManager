using Microsoft.AspNetCore.Mvc;
using server.Dto;

namespace server.Services.Interfaces
{
    public interface IPasswordService
    {
        public Task<List<PasswordAddDto>> GetPasswords([FromBody] PasswordAddDto passwordDto);
        public Task<bool> AddPassword(PasswordAddDto passwordDto);
        public Task<bool> DeletePassword(PasswordAddDto passwordDto);
        public Task<bool> UpdatePassword(PasswordAddDto passwordDto);
    }
}
