using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public  interface IBookingRepository :IGenericRepository<Booking>
    {

        Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(int customerId);

        Task<IReadOnlyList<Booking>> GetByProviderIdAsync(int providerId);

        Task<IReadOnlyList<Booking>> GetByStatusAsync(BookingStatus status);

        Task<IReadOnlyList<Booking>> GetCompletedBookingsAsync();
    }
}
