using BookingForHumanService.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.DTOs.AuthDtos
{
    public  class RegisterDto
    {
        public string Name { get; set; } = "";

        public string Email { get; set; } = "";
        public string Password { get; set; } = "";  
        public UserRole role { get; set; }

    }
}
