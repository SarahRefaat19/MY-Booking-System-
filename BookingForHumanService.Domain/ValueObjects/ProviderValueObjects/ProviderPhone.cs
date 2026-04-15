using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.ValueObjects.ProviderValueObjects
{
    public  sealed class ProviderPhone
    {

        public string Phone { get; }
        public ProviderPhone(string phone)
        {

            Phone = phone;
        }

        public static ProviderPhone Create(string phone)

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

            return new ProviderPhone(phone);
        }
        public override int GetHashCode() => Phone.GetHashCode();

    }
}
 

