using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Services.Interfaces;

namespace server.Services
{
    public class PasswordService : IPasswordService
    {
        public Task<bool> AddPassword(PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePassword(PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePassword(PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<PasswordDto>> GetPasswords([FromBody] PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePassword(PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }
    }
}
