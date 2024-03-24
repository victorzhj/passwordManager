using server.Dto;
using server.Models;
using server.Services.Interfaces;

namespace server.Services
{
    public class UserService : IUserService
    {
        public UserService() { }

        public User Delete(UserDto user)
        {
            throw new NotImplementedException();
        }

        public string GetSalt(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public User Login(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public User Register(UserCreationDto UserCreationDto)
        {
            throw new NotImplementedException();
        }
    }
}
