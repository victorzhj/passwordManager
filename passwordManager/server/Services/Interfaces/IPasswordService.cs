using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;

namespace server.Services.Interfaces
{
    public interface IPasswordService
    {
        public Task<List<PasswordAddDto>> GetPasswords(int userId);
        public Task<bool> AddPassword(PasswordAddDto passwordAddDto);
        public Task<bool> DeletePassword(PasswordIdDto passwordIdDto);
        public Task<bool> UpdatePassword(PasswordAddDto passwordDto);
    }
}
