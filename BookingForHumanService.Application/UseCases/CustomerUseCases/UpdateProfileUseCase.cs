using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;
using BookingForHumanService.Domain.ValueObjects.ProviderValueObjects;
using BookingForHumanService.Application.DTOs.CustomerDtos;

namespace BookingForHumanService.Application.UseCases.CustomerUseCases
{
    public class UpdateProfileUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
    
    public UpdateProfileUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerReadDto> UpdateProfile(CustomerUpdateDto customerUpdateDto)
        {
            // Get
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerUpdateDto.Id);

            // Check
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer Not Found");
            }

            // Value Objects
            var nameVO = CustomerName.Create(customerUpdateDto.Name);
            var emailVO = CustomerEmail.Create(customerUpdateDto.Email);
            var phoneVO = CustomerPhone.Create(customerUpdateDto.Phone);

            // Domain Update
            customer.UpdateProfile(nameVO, emailVO, phoneVO);

            // Save
            await _unitOfWork.SaveChangesAsync();

            // Map
            return _mapper.Map<CustomerReadDto>(customer);
        }
    }
}
