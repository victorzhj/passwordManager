using AutoMapper;
using server.Dao;
using server.Dto;
using server.Exceptions;
using server.Models;
using server.Services.Interfaces;

namespace server.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenDao<User> _userDao;
        private readonly IGenDao<Password> _passwordDao;
        
        public UserService(
            IMapper mapper,
            IGenDao<User> _userDao,
            IGenDao<Password> _passwordDao
            ) 
        {
            this._mapper = mapper;
            this._userDao = _userDao;
            this._passwordDao = _passwordDao;
        }

        public async Task<UserDto> Delete(UserDto userDto)
        {
            await _userDao.DeleteAsync(userDto.UserId);
            await _passwordDao.DeleteCustomAsync(filter: (password) => 
                password.UserId.Equals(userDto.UserId));
            return userDto;
        }

        public async Task<UserLoginDto> GetSalt(UserLoginDto userLoginDto)
        {
            var user = await _userDao.GetAllAsync(filter: (user) => 
                           user.Username.Equals(userLoginDto.Username));
            if (user.Count == 0)
            {
                throw new NotFoundException("User not found");
            } 
            var tempUserLoginDto = _mapper.Map<UserLoginDto>(user[0]);
            return tempUserLoginDto;
        }

        // needs to return with MasterPasswordHashed
        public async Task<UserDto> Login(UserDto userDto)
        {
            var user = await _userDao.GetByIdAsync(userDto.UserId);
            var password = await _passwordDao.GetAllAsync(filter: (password) => 
                                      password.UserId.Equals(userDto.UserId)
                                      && password.IsMasterPassword == true);
            if (password.Count == 0)
            {
                throw new NotFoundException("Master password not found");
            }
            var tempUserDto = _mapper.Map<UserDto>(user);
            tempUserDto.MasterPasswordHashed = password[0].EncryptedPassword;
            return tempUserDto;
        }
        // Doesn't need to return with MasterPasswordHashed
        public async Task<UserDto> Register(UserCreationDto UserCreationDto)
        {
            await _userDao.AddAsync(_mapper.Map<User>(UserCreationDto));
            var userDto = _mapper.Map<UserDto>(UserCreationDto);
            userDto.MasterPasswordHashed = null;
            return userDto;
        }
    }
}
