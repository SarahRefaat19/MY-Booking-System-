using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.DTOs.AuthDtos
{
    public  class AuthResponseDto
    {
        public string? Token { get; set; } 
        public string? RefreshToken { get; set; }
    }
}
