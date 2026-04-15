using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingForHumanService.Application.DTOs.CustomerDtos
{
    public class CustomerAddDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength (100)]
        [MinLength(10)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength (10)]
        public string Phone { get; set; }

    }
}
