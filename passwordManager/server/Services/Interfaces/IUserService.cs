using server.Models;
using server.Dto.UserDtos;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UsernameDto> Register(UserCreationDto userCreationDto);
        public Task<UserSaltDto> GetSalt(UsernameDto usernameDto);
        public Task<UsernameDto> Login(UserLoginDto userLoginDto);
        public Task Delete(UserDeletationDto userDeletationDto);
    }
}