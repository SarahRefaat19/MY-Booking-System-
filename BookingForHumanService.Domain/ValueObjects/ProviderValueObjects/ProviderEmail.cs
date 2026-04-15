using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BookingForHumanService.Domain.ValueObjects.ProviderValueObjects
{
    public sealed class ProviderEmail
    {
        public string Value { get; }


        private ProviderEmail(string value)
        {
            Value = value;
        }

        public static ProviderEmail Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Email Is Required");
            email = email.Trim();

            if (!IsValidEmail(email))
                throw new ArgumentException("Email Is Not Valid ");

            return new ProviderEmail(email);


        }

        public static bool IsValidEmail(string email)
        {
            var shape = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, shape, RegexOptions.IgnoreCase);
        }
        // compare with value
        public override bool Equals(object? obj)
        {

            if (obj is not ProviderEmail other)
                return false;
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

        public override string ToString() => Value;



    }

}

