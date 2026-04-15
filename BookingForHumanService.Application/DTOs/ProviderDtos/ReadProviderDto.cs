using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.DTOs.ProviderDtos
{
    public  class ReadProviderDto
    {
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        public string ServiceType { get; set; } = "";
        public string? Description { get; set; }

        public int ExperienceYears { get; set; }

        public string City { get; set; } = "";
        public string Country { get; set; } = "";


        public List<string> ServiceCities { get; set; } 

    }
}
