using AutoMapper;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.CustomerUseCases
{
    public class ChangeStatusUseCase
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ChangeStatusUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<CustomerReadDto> ChangeStatus(CustomerReadDto customerReadDto)
        {
            //GET
            var Customer = await _unitOfWork.Customers.GetByIdAsync(customerReadDto.Id);
            //CHECK
            if (Customer == null)
            {
                throw new Exception("CustomerNotFound");
            }
            //APPLY ROLE
            Customer.ChangeStatus(customerReadDto.status);
            //SAVE
            await _unitOfWork.SaveChangesAsync();
            //MAP 
            return _mapper.Map<CustomerReadDto>(Customer);
        }
    }
}
