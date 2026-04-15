using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingForHumanService.Application.DTOs.ProviderDtos
{
    public class UpdateProviderDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Phone { get; set; } = "";

     
    }
}
