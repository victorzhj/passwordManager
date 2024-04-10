using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using server.Dao;
using server.Dto;
using server.Dto.UserDtos;
using server.Exceptions;
using server.Models;
using server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace server.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenDao<User> _userDao;
        private readonly IGenDao<Password> _passwordDao;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;
        public UserService(
            IConfiguration configuration,
            IMapper mapper,
            IGenDao<User> _userDao,
            IGenDao<Password> _passwordDao,
            IPasswordService passwordService
            )
        {
            this._configuration = configuration;
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
            var masterPassword = new Password
            {
                UserId = user.UserId,
                Salt = userCreationDto.Salt,
                IsMasterPassword = true,
                EncryptedPassword = userCreationDto.MasterPasswordHashed
            }; 
            
            await _passwordService.AddPassword(masterPassword);
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

        public async Task<TokenDto> Login(UserLoginDto userLoginDto)
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
            var DerivedKeySalt = users[0].DerivedKeySalt;
            var token = GenerateJwtToken(users[0]);
            var tokenDto = new TokenDto
            {
                AccessToken = token.Item1,
                DerivedKeySalt = DerivedKeySalt,
                ExpiresIn = token.Item2
            };
            return tokenDto;
        }
        public async Task<bool> Delete(int userId)
        {
            await _userDao.DeleteAsync(userId);
            bool deleted = await _passwordDao.DeleteCustomAsync(filter: (password) =>
                password.UserId.Equals(userId));
            return deleted;
        }

        private (string, int) GenerateJwtToken(User user)
        {
            var expireTime = 30;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireTime),
                signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), expireTime * 60);
        }

    }
}
