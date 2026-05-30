using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;

namespace BookingForHumanService.Domain.Entities
{
    public  class Customer
    {
        //Basic Deatails-----------------------------
        public int Id { get; private set; }
        public int UserId { get; private set; }   
        public User User { get; private set; }
        public CustomerName Name { get; private set; }
        public CustomerEmail Email { get; private set; }
        public CustomerPhone Phone { get; private set; }
        public CustomerStatus Status { get; private set; }
        private List<int> _favouriteProviderIds = new(); // Return Again 

        //Relations ----------------------------------


        private List<Booking> _bookings = new();
        private List<Review> _reviews = new();
        private List<Address> _addresses = new();
        private List<Notification> _notifications = new();


        //For Encapsulation----------------------------

        public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();
        public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();
        public IReadOnlyList<Address> Addresses => _addresses.AsReadOnly();
        public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();

        public Customer(User user, CustomerName name, CustomerEmail email, CustomerPhone phone, CustomerStatus status)
        {

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Status = status;
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
        private Customer() { } // EF Core only

        //Busniss rules--------------------
        public void UpdateProfile(CustomerName name, CustomerEmail email, CustomerPhone phone)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        }
  

        public void AddBooking(Booking booking){
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));
            _bookings.Add(booking);
        }

        public void AddReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            if (Status != CustomerStatus.Active)
                throw new InvalidOperationException("Only active customers can add reviews");

            _reviews.Add(review);
        }

        public void AddAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            if (address.IsDefault)
                foreach (var addr in _addresses)
                    addr.RemoveDefault();

            _addresses.Add(address);
        }

        public void SetDefaultAddress(int addressId)
        {
            var target = _addresses.FirstOrDefault(a => a.Id == addressId)
                ?? throw new InvalidOperationException("Address not found");
            foreach (var addr in _addresses)
                addr.RemoveDefault();

            target.SetAsDefault();
        }

        public void ChangeStatus(CustomerStatus status)
        {
            if (Status == CustomerStatus.Blocked)
                throw new InvalidOperationException("Blocked customer Status cannot change status");

            Status = status;
        }

    }
}

