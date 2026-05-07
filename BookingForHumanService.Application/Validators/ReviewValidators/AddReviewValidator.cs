using BookingForHumanService.Application.DTOs.ReviewDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Validators.ReviewValidators
{
    public class AddReviewValidator : AbstractValidator<AddReviewDto>
    {
        public AddReviewValidator()
        {
            RuleFor(r => r.BookingId)
                .GreaterThan(0);

            RuleFor(r => r.Rating)
                .InclusiveBetween(0, 5);

            RuleFor(r => r.Comment)
                .MaximumLength(1000);
        }
    }
}
