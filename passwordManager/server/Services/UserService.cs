using AutoMapper;
using server.Dao;
using server.Dto;
using server.Dto.UserDtos;
using server.Exceptions;
using server.Models;
using server.Services.Interfaces;
using System.Security.Cryptography;

namespace server.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenDao<User> _userDao;
        private readonly IGenDao<Password> _passwordDao;
        private readonly IPasswordService _passwordService;
        public UserService(
            IMapper mapper,
            IGenDao<User> _userDao,
            IGenDao<Password> _passwordDao,
            IPasswordService passwordService
            )
        {
            this._mapper = mapper;
            this._userDao = _userDao;
            this._passwordDao = _passwordDao;
            _passwordService = passwordService;
        }

        public async Task<UsernameDto> Register(UserCreationDto userCreationDto)
        {
            var users = await _userDao.GetAllAsync(filter: (user) =>
                user.Username.Equals(userCreationDto.Username));
            if (users.Count != 0)
            {
                throw new ConflictException("Username already exists");
            }
            var user = await _userDao.AddAsync(_mapper.Map<User>(userCreationDto));
            var passwordAddDto = new PasswordAddDto
            {
                UserId = user.UserId,
                Salt = userCreationDto.Salt,
                IsMasterPassword = true,
                EncryptedPassword = userCreationDto.MasterPasswordHashed
            }; 
            
            await _passwordService.AddPassword(passwordAddDto);
            var usernameDto = _mapper.Map<UsernameDto>(userCreationDto);
            usernameDto.UserId = user.UserId;
            return usernameDto;
        }

        public async Task<UserSaltDto> GetSalt(string username)
        {
            var users = await _userDao.GetAllAsync(filter: (user) => 
                user.Username.Equals(username));
            if (users.Count == 0)
            {
                throw new NotFoundException("User not found");
            } 
            var masterPassword = await _passwordDao.GetAllAsync(filter: (password) =>
                password.UserId.Equals(users[0].UserId)
                && password.IsMasterPassword == true);
            var mastesPasswordSalt = masterPassword[0].Salt;
            var userSaltDto = new UserSaltDto
            {
                Salt = mastesPasswordSalt
            };
            return userSaltDto;
        }

        public async Task<UsernameDto> Login(UserLoginDto userLoginDto)
        {
            var users = await _userDao.GetAllAsync(filter: (user) => 
                           user.Username.Equals(userLoginDto.Username));
            var userId = users[0].UserId; 
            var masterPassword = await _passwordDao.GetAllAsync(filter: (password) =>
                password.UserId.Equals(userId)
                && password.IsMasterPassword == true
                && password.EncryptedPassword == userLoginDto.MasterPasswordHashed);
            if (masterPassword.Count == 0)
            {
                throw new ConflictException("Masterpassword not a match");
            }
            var usernameDto = _mapper.Map<UsernameDto>(users[0]);
            return usernameDto;
        }
        public async Task<bool> Delete(UserDeletationDto userDeletationDto)
        {
            await _userDao.DeleteAsync(userDeletationDto.UserId);
            bool deleted = await _passwordDao.DeleteCustomAsync(filter: (password) =>
                password.UserId.Equals(userDeletationDto.UserId));
            return deleted;
        }
    }
}
