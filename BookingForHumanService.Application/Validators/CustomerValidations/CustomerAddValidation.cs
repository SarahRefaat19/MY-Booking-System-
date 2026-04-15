using FluentValidation;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.Validators.CustomerValidations
{
   public  class CustomerAddValidation : AbstractValidator<CustomerAddDto>
    {
        public CustomerAddValidation() 
        
        
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required")
           .MaximumLength(100).WithMessage("Name too long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone is not valid");
        }
    }

}

   