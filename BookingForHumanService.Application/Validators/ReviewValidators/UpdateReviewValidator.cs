using BookingForHumanService.Application.DTOs.ReviewDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Validators.ReviewValidators
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(r => r.Comment)
                .MaximumLength(1000);
        }
    }
}
