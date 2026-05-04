using AutoMapper;
using BookingForHumanService.Application.DTOs.BookingDtos;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Mapping
{
    public  class BookingMapping :Profile
    {

      

            public BookingMapping()
            {
                CreateMap<AddBookingDto, Booking>();
                CreateMap<ReadBookingDto, Booking>();
                CreateMap<Booking, UpdateBookingDto>().ReverseMap();

                CreateMap<Booking, DeleteBookingDto>().ReverseMap();

            }
        }

    }
