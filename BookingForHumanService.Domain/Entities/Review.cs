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
        public Provider Provider { get; private set; }
        public Customer Customer { get; private set; }
        private Review() { } 

        public Review(Provider provider, Customer customer, double rating, string comment)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));


            Customer = customer ?? throw new ArgumentNullException(nameof(customer));

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

