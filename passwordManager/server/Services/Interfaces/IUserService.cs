using server.Models;
using server.Dto;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        User Register(UserDto user);
        User Login(UserDto user);
        User Delete(UserDto user);
    }
}
