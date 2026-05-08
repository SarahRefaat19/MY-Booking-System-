using System;
using System.Collections.Generic;
using System.Text;
using BookingForHumanService.Domain.Enums;
using Microsoft.AspNetCore.Identity;


namespace BookingForHumanService.Domain.Entities
{
  
public class User :IdentityUser<int>
    {

     
        public UserRole Role { get;  set; } 

        public string? RefreshToken  { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
     
       

        

        //public User( int id, string hashPassword, UserRole role, string? refreshToken, DateTime? refreshTokenExpiryTime)
        //{
        //    Id = id;

        //    HashPassword = hashPassword;
        //    Role = role;
        //    RefreshToken = refreshToken;
        //    RefreshTokenExpiryTime = refreshTokenExpiryTime;
        //}
        public static User Create(int id, string hashPassword, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(hashPassword))
                throw new Exception("Password is required");

            return new User
            {
                Role = role
            };
        }
        public void SetRefreshToken(string refreshToken, DateTime expiryTime)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = expiryTime;
        }

    
    }

}

