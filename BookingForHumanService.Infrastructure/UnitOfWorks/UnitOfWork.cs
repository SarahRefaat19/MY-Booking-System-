using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;

namespace BookingForHumanService.Infrastructure.UnitOfWorks
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context;

        public UnitOfWork(
        BookingDbContext context,
        ICustomerRepository customers,
        IProviderRepository providers,
        IBookingRepository bookings,
        IReviewRepository reviews,
        IAddressRepository addresses,
        INotificationRepository notifications,
        IUserNotificationRepository userNotifications)
        {
            _context = context;
            Customers = customers;
            Providers = providers;
            Bookings = bookings;
            Reviews = reviews;
            Addresses = addresses;
            Notifications = notifications;
            UserNotifications = userNotifications;
        }

        public ICustomerRepository Customers { get; }

        public IUserNotificationRepository UserNotifications { get; }
        public IProviderRepository Providers { get; }
        public IBookingRepository Bookings { get; }
        public IReviewRepository Reviews { get; }
        public IAddressRepository Addresses { get; }
        public INotificationRepository Notifications { get; }

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}
