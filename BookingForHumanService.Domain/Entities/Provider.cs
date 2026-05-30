using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;
using BookingForHumanService.Domain.ValueObjects.ProviderValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Entities
{
    public class Provider
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public ProviderName Name { get; private set; }
        public ProviderEmail Email { get; private set; }
        public ProviderPhone Phone { get; private set; } 
        public string ServiceType { get; private set; } 
        public string Description { get; private set; }
        public int ExperienceYears { get; private set; }
        public double Rating { get; private set; }
        public int ReviewsCount { get; private set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public List<string> ServiceCities { get; private set; } = new();

        private readonly List<Review> _reviews = new();
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
        public List<Booking> Bookings { get; private set; } = new();

        public Provider(User user, ProviderName name, string serviceType, ProviderEmail email, ProviderPhone phone)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ServiceType = string.IsNullOrEmpty(serviceType) ? 
                throw new ArgumentNullException("Service Type Can not be Embty") : serviceType;

            Email = email;
            Phone = phone;

            IsActive = true;
            IsAvailable = true;
        }
        private Provider() { }
        public void UpdateProfile(ProviderName name, ProviderEmail email, ProviderPhone phone)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        }
        public void UpdateAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
        public void AddReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            if (!IsActive)
                throw new InvalidOperationException("Inactive Provider can not receive reviews. ");

            _reviews.Add(review);

            ReviewsCount++; // Increase Reviews Count

            Rating = Reviews.Average(review => review.Rating); // Update Rating
        }
        public void AddBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));
            Bookings.Add(booking);
        }
        public void AddAddressDetails(string? city, string? country, List<string> serviceCites)
        {
            City = city ?? "No City Specified";
            Country = country ?? "No Country Specified";
            ServiceCities = serviceCites;
        }
    }
}