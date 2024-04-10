using server.Models;
using server.Dto.UserDtos;
using System.Security.Claims;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UsernameDto> Register(UserCreationDto userCreationDto);
        public Task<UserSaltDto> GetSalt(string username);
        public Task<TokenDto> Login(UserLoginDto userLoginDto);
        public Task<bool> Delete(int userId);
    }
}