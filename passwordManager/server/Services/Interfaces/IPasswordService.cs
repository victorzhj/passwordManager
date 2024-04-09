﻿using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Dto.PasswordDtos;
using server.Dto.UserDtos;
using server.Models;

namespace server.Services.Interfaces
{
    public interface IPasswordService
    {
        public Task<List<PasswordAddDto>> GetPasswords(int userId);
        public Task<bool> AddPassword(PasswordAddDto passwordAddDto);
        public Task<bool> AddPassword(Password password);
        public Task<bool> DeletePassword(PasswordIdDto passwordIdDto);
        public Task<bool> UpdatePassword(PasswordAddDto passwordDto);
    }
}
