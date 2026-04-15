using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using AutoMapper;

namespace BookingForHumanService.Application.Mapping
{
    public class CustomerMapping: Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerAddDto, Customer>();
            CreateMap<CustomerReadDto, Customer>();
            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();

            CreateMap<Customer, CustomerDeleteDto>().ReverseMap();

        }
    }
}


