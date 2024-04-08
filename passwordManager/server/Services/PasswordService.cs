using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Dao;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;
using server.Models;
using server.Services.Interfaces;
using System.Linq;

namespace server.Services
{
    
    public class PasswordService : IPasswordService
    {
        private readonly IMapper _mapper;
        private readonly IGenDao<Password> _passwordDao;
        public PasswordService(
            IMapper mapper, 
            IGenDao<Password> passwordDao)
        {
            this._mapper = mapper;
            this._passwordDao = passwordDao;
        }
        public async Task<bool> AddPassword(PasswordAddDto passwordAddDto)
        {
            var password = await _passwordDao.AddAsync(_mapper.Map<Password>(passwordAddDto));
            if (password == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeletePassword(PasswordIdDto passwordIdDto)
        {
            return await _passwordDao.DeleteAsync(passwordIdDto.PasswordId);
        }

        public async Task<List<PasswordAddDto>> GetPasswords(int userId)
        {
            var passwords = await _passwordDao.GetAllAsync(filter: (password) => 
                password.UserId == userId
                && password.IsMasterPassword == false);
            return passwords.Select(password => _mapper.Map<PasswordAddDto>(password)).ToList();
        }

        public Task<bool> UpdatePassword(PasswordAddDto passwordDto)
        {
            throw new NotImplementedException();
        }
    }
}
