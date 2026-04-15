using System;
using System.Collections.Generic;
using System.Text;
using BookingForHumanService.Domain.Enums;


namespace BookingForHumanService.Domain.Entities
{
  
public class User
    {

        public int Id { get; set; }
     
        public string HashPassword { get;  set; } = "";
        public UserRole Role { get; private set; } 

        public string? RefreshToken  { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        private User()
        {

        }
        public User( int id, string hashPassword, UserRole role, string? refreshToken, DateTime? refreshTokenExpiryTime)
        {
            Id = id;
            
            HashPassword = hashPassword;
            Role = role;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
        public static User Create(int id, string hashPassword, UserRole role, string? refreshToken, DateTime? refreshTokenExpiryTime)
        {
            if (string.IsNullOrWhiteSpace(hashPassword)) throw new Exception("Passwrod Is Required ");
            return new User { HashPassword = hashPassword, Role = role };
        }

        public void SetRefreshToken(string refreshToken, DateTime expiryTime)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = expiryTime;
        }

    
    }

}

