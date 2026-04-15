using BookingForHumanService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.DTOs.CustomerDtos
{
   public class CustomerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CustomerStatus status { get; set; }
    }
}
