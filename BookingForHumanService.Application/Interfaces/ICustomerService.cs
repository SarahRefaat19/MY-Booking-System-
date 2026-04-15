using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.Services;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace BookingForHumanService.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerReadDto> CreateCustomerAsync(CustomerAddDto customerAddDto);
        Task<CustomerReadDto> GetCustomerByIdAsync(int Id);
        Task<IReadOnlyList<CustomerReadDto>> GetAllCustomersAsync();
        Task DeleteCustomerAsync(int Id);
        Task<CustomerReadDto> UpdateCustomerAsync(int Id,CustomerUpdateDto customerUpdateDto);
        Task<IReadOnlyList<CustomerReadDto>> GetCustomersPagedAsync(int pageNumber, int pageSize);
        Task <int> GetTotalCustomersCountAsync();




    }
}
