using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.DTOs.ReviewDtos
{
    public class AddReviewDto
    {
        public int BookingId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
