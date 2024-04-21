using AutoMapper;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;
using server.Models;

namespace server.AutoMapperConfiguration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserSaltDto>();
            CreateMap<UserSaltDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserLoginDto>();
            CreateMap<UserCreationDto, User>();
            CreateMap<User, UserCreationDto>();
            CreateMap<UserDeletationDto, User>();
            CreateMap<User, UserDeletationDto>();
            CreateMap<UsernameDto, User>();
            CreateMap<User, UsernameDto>();
            CreateMap<UsernameDto, UserCreationDto>();
            CreateMap<UserCreationDto, UsernameDto>();
            CreateMap<Password, PasswordAddDto>();
            CreateMap<PasswordAddDto, Password>();
            CreateMap<UserCreationDto, PasswordAddDto>();
            CreateMap<PasswordAddDto, UserCreationDto>();
            CreateMap<Password, PasswordDetailsDto>();
            CreateMap<PasswordDetailsDto, Password>();
        }
    }
}
