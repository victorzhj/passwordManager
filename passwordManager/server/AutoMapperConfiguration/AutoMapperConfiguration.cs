using AutoMapper;
using server.Dto;
using server.Models;

namespace server.AutoMapperConfiguration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserLoginDto>();
            CreateMap<UserCreationDto, User>();
            CreateMap<User, UserCreationDto>();
            CreateMap<Password, PasswordDto>();
            CreateMap<PasswordDto, Password>();
        }
    }
}
