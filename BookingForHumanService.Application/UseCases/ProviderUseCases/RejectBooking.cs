using AutoMapper;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.ProviderUseCases
{
    public class RejectBooking
    {

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private readonly IProviderService _providerService;


        public RejectBooking(IMapper mapper, IUnitOfWork unitOfWork, IProviderService providerService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _providerService = providerService;

        }

   
            public async Task ExecuteAsync(int bookingId)
            {
                var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);

                if (booking == null)
                    throw new Exception("Booking not found");

                if (booking.Status != BookingStatus.Pending)
                    throw new Exception("Booking is not pending");

                booking.Reject();

                await _unitOfWork.Bookings.UpdateAsync(booking);
                await _unitOfWork.SaveChangesAsync();
            }
        }
}
    