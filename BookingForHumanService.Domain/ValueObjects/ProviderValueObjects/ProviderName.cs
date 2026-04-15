using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using BookingForHumanService.Domain.Entities;

namespace BookingForHumanService.Domain.ValueObjects.ProviderValueObjects
{
    public  sealed class ProviderName
    {
        public string Name { get; }
        private ProviderName(string name)
        {
            Name = name;
        }

        public static ProviderName Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(" Enter Your Name ");
            }
            name = name.Trim();

            if (name.Length < 3)
            {
                throw new ArgumentNullException(" Your Name is too Short ");

            }
            if (name.Any(char.IsDigit))
            {
                throw new Exception("Do not Enter Numbers");
            }


            return new ProviderName(name);

        }

        public override int GetHashCode() => Name.ToLowerInvariant().GetHashCode();


    }
}

