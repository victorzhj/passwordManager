using server.Models;
using server.Dto;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        public User Register(UserCreationDto UserCreationDto);
        public string GetSalt(UserLoginDto userLoginDto);
        public User Login(UserDto userDto);
        public User Delete(UserDto userDto);
    }
}