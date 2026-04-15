
using BookingForHumanService.Application.DTOs.CustomerDtos;
using FluentValidation;

namespace BookingForHumanService.Application.Validators.CustomerValidations
{
    public class CustomerUpdateValidation : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateValidation()


        {
            RuleFor(n => n.Name).MaximumLength(100).WithMessage("Name is too long ");
            RuleFor(n => n.Phone).MaximumLength(20).MinimumLength(10).WithMessage("Name is too long ");
            RuleFor(n => n.Email).MaximumLength(50).NotEmpty().EmailAddress();




        }
    }
}
