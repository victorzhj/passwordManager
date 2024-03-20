using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Services.Interfaces;

namespace server.Services
{
    public class PasswordService : IPasswordService
    {
        public bool AddPassword([FromBody] PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public bool DeletePassword([FromBody] PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<PasswordDto>> GetPasswords([FromBody] PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassword([FromBody] PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }
    }
}
