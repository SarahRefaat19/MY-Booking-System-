using AutoMapper;
using BookingForHumanService.Application.DTOs.ReviewDtos;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReadReviewDto>();

            CreateMap<AddReviewDto, Review>();

            CreateMap<UpdateReviewDto, Review>();
        }
    }
}
