using Microsoft.AspNetCore.Mvc;
using server.Dto;

namespace server.Services.Interfaces
{
    public interface IPasswordService
    {
        public Task<List<PasswordDto>> GetPasswords([FromBody] PasswordDto passwordDto);
        public bool AddPassword([FromBody] PasswordDto passwordDto);
        public bool DeletePassword([FromBody] PasswordDto passwordDto);
        public bool UpdatePassword([FromBody] PasswordDto passwordDto);
    }
}
