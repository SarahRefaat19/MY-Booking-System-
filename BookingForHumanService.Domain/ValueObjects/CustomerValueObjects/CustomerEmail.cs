using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BookingForHumanService.Domain.ValueObjects.CustomerValueObjects
{
 public  sealed class CustomerEmail
    {
        public string Value { get; }


        private CustomerEmail(string value)
        {
            Value = value;
        }

        public static CustomerEmail Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Email Is Required");
            email = email.Trim();

            if (!IsValidEmail(email))
                throw new ArgumentException("Email Is Not Valid ");

            return new CustomerEmail(email);


        }

        public static bool IsValidEmail(string email)
        {
            var shape = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, shape ,RegexOptions.IgnoreCase);
        }
        // compare with value
        public override bool Equals(object? obj)
            {

            if (obj is  not CustomerEmail other )
                return false;
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
            }

        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

        public override string ToString() => Value;



        }

    }

