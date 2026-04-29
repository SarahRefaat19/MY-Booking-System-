using AutoMapper;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.Mapping
{
    public  class ProviderMapping: Profile
    {

        public ProviderMapping()
        {
            CreateMap<AddProviderDto, Provider>();
            CreateMap<ReadProviderDto, Provider>();
            CreateMap<Provider, UpdateProviderDto>().ReverseMap();

            CreateMap<Provider, DeleteProviderDto>().ReverseMap();

        }
    }

}

