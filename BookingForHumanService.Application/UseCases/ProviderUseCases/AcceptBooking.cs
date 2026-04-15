using AutoMapper;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.ProviderUseCases
{
    public class AcceptBooking
    {
        private IUnitOfWork _unitOfWork;
        private readonly IProviderService _providerService;


        public AcceptBooking( IUnitOfWork unitOfWork, IProviderService  providerService)
        {
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

            booking.Accept();

            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
