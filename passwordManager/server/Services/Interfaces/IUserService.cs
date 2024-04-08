using server.Models;
using server.Dto.UserDtos;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UsernameDto> Register(UserCreationDto userCreationDto);
        public Task<UserSaltDto> GetSalt(string username);
        public Task<UsernameDto> Login(UserLoginDto userLoginDto);
        public Task<bool> Delete(UserDeletationDto userDeletationDto);
    }
}