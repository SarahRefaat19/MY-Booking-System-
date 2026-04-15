using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingForHumanService.Application.DTOs.ProviderDtos
{
    public  class DeleteProviderDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string email { get; set; } = "";
        [Required]

        public string phone { get; set; } = "";
    }
}
