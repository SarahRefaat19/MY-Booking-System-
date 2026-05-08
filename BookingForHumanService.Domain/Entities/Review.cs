using BookingForHumanService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Entities
{
 public class Review
    {
        public int Id { get; private set; }
      
        public double Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        public int BookingId { get; private set; }
        public Booking Booking { get; private set; }

        /// <Why??>
        /// becuase Every Review On Specific Booking 
        /// Every Booking has : Provider and Customer Already 
        /// </why??>

        //public Provider Provider { get; private set; }
        //public Customer Customer { get; private set; }


        /// for analysis and support the collections exist at customer and provider [forgien key]
        public int CustomerId { get; private set; }
        public int ProviderId { get; private set; }
        private Review() { } 

        public Review(Booking booking, double rating, string comment)
        {

            Booking = booking ?? throw new ArgumentNullException(nameof(booking));

            // ده اسمه C# Record 
            (BookingId, ProviderId, CustomerId) =
            (booking.Id, booking.ProviderId, booking.CustomerId);

            if (rating < 0 || rating > 5)
                throw new ArgumentException("Rating must be between 0 and 5", nameof(rating));

            Rating = rating;
            Comment = comment ?? string.Empty;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateComment(string comment)
        {
            Comment = comment ?? Comment;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}

