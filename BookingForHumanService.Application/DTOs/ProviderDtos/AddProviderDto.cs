using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingForHumanService.Application.DTOs.ProviderDtos
{
    public  class AddProviderDto
    {
        [Required]

        public string Name { get; set; } = "";
        [Required]

        public string Email { get; set; } = "";
        [Required]

        public string Phone { get; set; } = "";
        [Required]


        public string ServiceType { get; set; } = "";


        public string? Description { get; set; }

        public int ExperienceYears { get; set; }
        [Required]

        public string City { get; set; } = "";
        [Required]

        public string Country { get; set; } = "";

        public required List<string> ServiceCities { get; set; }
    }
}
