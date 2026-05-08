using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Interfaces
{
    public interface IBookingService
    {



        Task<Booking> CreateBookingAsync(int customerId, int providerId, DateTime serviceDate, decimal price, string details);

        Task AcceptBookingAsync(int bookingId);
        Task RejectBookingAsync(int bookingId);
        Task StartBookingAsync(int bookingId);
        Task CancelBookingAsync(int bookingId);
        Task CompleteBookingAsync(int bookingId);

        Task<IReadOnlyList<Booking>> GetByCustomerAsync(int customerId);
        Task<IReadOnlyList<Booking>> GetByProviderAsync(int providerId);
        Task<IReadOnlyList<Booking>> GetByStatusAsync(BookingStatus status);
    }
}
