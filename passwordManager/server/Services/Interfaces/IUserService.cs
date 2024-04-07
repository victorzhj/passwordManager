using server.Models;
using server.Dto;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(UserCreationDto UserCreationDto);
        public Task<UserLoginDto> GetSalt(UserLoginDto userLoginDto);
        public Task<UserDto> Login(UserDto userDto);
        public Task<UserDto> Delete(UserDto userDto);
    }
}