using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Interfaces;

namespace BookingForHumanService.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProviderRepository _providerRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IProviderRepository providerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _providerRepository = providerRepository;
        }

        public async Task<Booking> CreateBookingAsync(int customerId, int providerId, DateTime serviceDate, decimal price, string details)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            var provider = await _providerRepository.GetByIdAsync(providerId);

            if (customer == null)
                throw new Exception("Customer not found");

            if (provider == null)
                throw new Exception("Provider not found");

            var booking = Booking.Create(customer, provider, serviceDate, price, details);

            return await _bookingRepository.AddAsync(booking);
        }


        public async Task AcceptBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            booking.Accept();
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task RejectBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            booking.Reject();
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task StartBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            booking.Start();
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            booking.Cancel();
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task CompleteBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            booking.Complete();
            await _bookingRepository.UpdateAsync(booking);
        }


        public async Task<IReadOnlyList<Booking>> GetByCustomerAsync(int customerId)
        {
            return await _bookingRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<IReadOnlyList<Booking>> GetByProviderAsync(int providerId)
        {
            return await _bookingRepository.GetByProviderIdAsync(providerId);
        }

        public async Task<IReadOnlyList<Booking>> GetByStatusAsync(BookingStatus status)
        {
            return await _bookingRepository.GetByStatusAsync(status);
        }
    }
}