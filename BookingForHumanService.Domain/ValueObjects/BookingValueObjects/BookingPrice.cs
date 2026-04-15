using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.ValueObjects.BookingValueObjects
{
    public  class BookingPrice
    {

        decimal value { get; set; }


        public BookingPrice(decimal price)
        {
            if (value <= 0)

                throw new ArgumentException("Price must be greater than 0 ");

           value = price;
            
        }


       



    }
}
