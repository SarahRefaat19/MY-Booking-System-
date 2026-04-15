using BookingForHumanService.Domain.Enums;
using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace BookingForHumanService.Domain.Entities
{
    public  class Booking
    {
          
            public int Id { get; private set; }
        //Relations
            public Customer Customer { get; private set; }
        public Provider Provider { get;  set; }
        public int ProviderId { get; private set; }
        public int CustomerId { get; private set; }

            public DateTime BookingDate { get; private set; }
            public DateTime ServiceDate { get; private set; }

            public BookingStatus Status { get; private set; }

            public decimal Price { get; private set; }

            public string Details { get; private set; }

        private Booking() { }

        private Booking(Customer _Customer, Provider _Provider,  DateTime _ServiceDate, decimal _Price, string _Details)
        {
            Customer = _Customer ?? throw new ArgumentNullException(nameof(_Customer));
            Provider = _Provider ?? throw new ArgumentNullException(nameof(_Provider));

            BookingDate = DateTime.UtcNow;
            ServiceDate = _ServiceDate;
            Status = BookingStatus.Pending;
            Price = _Price;
            Details = _Details;
        }
        //Book 

        public static Booking Create(Customer customer, Provider provider, DateTime serviceDate, decimal price, string details)
        {
            if (serviceDate < DateTime.UtcNow)
                throw new ArgumentException("Invalid service date");

            return new Booking(customer, provider, serviceDate, price, details);
        }
        //start
        public void Start()
        {
            if (Status != BookingStatus.Accepted)
                throw new InvalidOperationException("Only accepted booking can start");

            Status = BookingStatus.InProgress;
        }
        //Cancel

        public void  Cancel()
        {
            //For Cancel Booking
            if (Status == BookingStatus.Completed || Status == BookingStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot cancel this booking");
            }
            Status = BookingStatus.Cancelled;
        }
        //Reject
        public void Reject()
        {
            if(Status!= BookingStatus.Pending)
            {
                throw new InvalidOperationException("Only Pending Booking Can Be Rejected ");

            }
            Status = BookingStatus.Rejected;
        }

        //Approve
        public void Accept()
        {
            if (Status != BookingStatus.Pending)
            {
                throw new InvalidOperationException("Only Pending Booking Can Be Approved ");

            }
            Status = BookingStatus.Accepted;
        }



    }
           
        }
        