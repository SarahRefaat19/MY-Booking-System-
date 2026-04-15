using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingForHumanService.Domain.ValueObjects.CustomerValueObjects
{
    public sealed class CustomerName 
    {
        public string Name { get; }
        private CustomerName(string name)
        {
            Name = name;
        }

        public static CustomerName Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(" Enter Your Name ");
            }
            name=name.Trim();

            if(name.Length < 3)
            {
                throw new ArgumentNullException(" Your Name is too Short ");

            }
            if (name.Any(char.IsDigit))
            {
                throw new Exception("Do not Enter Numbers");
            }


            return new CustomerName(name);

        }

        public override int GetHashCode() => Name.ToLowerInvariant().GetHashCode();
     

         }   



    }

