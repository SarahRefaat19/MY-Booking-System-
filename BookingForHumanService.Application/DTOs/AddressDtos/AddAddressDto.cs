using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.DTOs.AddressDtos
{
    public class AddAddressDto
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
    }
}
