using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Services.Interfaces;

namespace server.Services
{
    public class PasswordService : IPasswordService
    {
        public Task<bool> AddPassword(PasswordAddDto passwordAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePassword(PasswordAddDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePassword(PasswordAddDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<PasswordAddDto>> GetPasswords([FromBody] PasswordAddDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePassword(PasswordAddDto passwordDto)
        {
            throw new NotImplementedException();
        }
    }
}
