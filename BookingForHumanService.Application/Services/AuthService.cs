using BookingForHumanService.Application.DTOs.AuthDtos;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Services
{
    public  class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWTService _jwtService;
        public AuthService(
               UserManager<User> userManager
               , JWTService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async  Task RegisterAsync([FromBody] RegisterDto registerDto)
        {
            if (registerDto.role == UserRole.Admin)
                throw new Exception("Not allowed to register as Admin");


            var user = new User
            {
                UserName = registerDto.Email, 

                Email = registerDto.Email,
                Role = registerDto.role
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));


            var rolename = registerDto.role.ToString();

          await  _userManager.AddToRoleAsync(user, rolename);


        }
        
        public async Task<AuthResponseDto> LoginAsync([FromBody] LoginDto loginDto)
        {
           
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                throw new Exception("Invalid email or password");

            var result = await _userManager.CheckPasswordAsync(
                user,
                loginDto.Password
            );

            if (!result)
                throw new Exception("Invalid email or password");


            var token = _jwtService.GenerateToken(user);
            var refreshToken = await _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

             await _userManager.UpdateAsync(user);
            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

     

    }
}
