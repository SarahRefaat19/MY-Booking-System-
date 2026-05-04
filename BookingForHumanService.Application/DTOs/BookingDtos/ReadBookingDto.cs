using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.DTOs.BookingDtos
{
    public class ReadBookingDto
    {
        public int CustomerId { get; set; }
        public int ProviderId { get; set; }

        public DateTime ServiceDate { get; set; }

        public decimal Price { get; set; }

        public string? Details { get; set; } 
    }
}
