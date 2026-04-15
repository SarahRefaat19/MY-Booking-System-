using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Entities
{
  public class Address
    {
        public int  Id { get; private set; }

        public bool IsDefault { get; private set; } 
        public int CustomerId { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string Street { get; private set; }
        public int HomeNumber { get; private set; }
        private Address() { }

        public Address(int customerId, string city, string region, string street, int homeNumber)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required");
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required");
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("region is required");
            CustomerId = customerId;
            City = city;
            Region = region;
            Street = street;
            HomeNumber = homeNumber;
            IsDefault = false;
        }

        internal void SetAsDefault() => IsDefault = true;
        internal void RemoveDefault() => IsDefault = false;

    }
}
