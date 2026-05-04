using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.DTOs.BookingDtos
{
    public class UpdateBookingDto
    {
        public int CustomerId { get; set; }
        public int ProviderId { get; set; }

    }
}
