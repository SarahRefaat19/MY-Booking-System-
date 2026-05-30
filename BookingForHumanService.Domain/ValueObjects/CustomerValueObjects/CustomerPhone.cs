using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.ValueObjects.CustomerValueObjects
{
    public sealed class CustomerPhone
    {
        public string Value { get; }
        public CustomerPhone(string phone)
        {

            Value = phone;
        }
        public CustomerPhone() { } // EF Core only
        public static CustomerPhone Create(string phone)

        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException("Enter Your Phone ");
            }
            if (!phone.All(char.IsDigit) && !phone.StartsWith("+"))
            {
                throw new ArgumentException("Invalid Number");
            }

            if (phone.Length < 7)
            {
                throw new ArgumentException("Too Short Number");
            }

            return new CustomerPhone(phone);
        }
        public override int GetHashCode() => Value.GetHashCode();

    }
}
