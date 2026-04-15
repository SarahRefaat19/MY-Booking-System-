using AutoMapper;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.Entities;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BookingForHumanService.Application.Services
{
public class CustomerService: ICustomerService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public CustomerService(IUnitOfWork unitofwork, IMapper mapper) { _unitofwork = unitofwork; _mapper = mapper; }
       public async Task<CustomerReadDto> CreateCustomerAsync(CustomerAddDto customerAddDto)
        {
            var customer= _mapper.Map<Customer>(customerAddDto);
             await _unitofwork.Customers.AddAsync(customer);
          return  _mapper.Map<CustomerReadDto>(customer);
        }
        public async Task<CustomerReadDto> GetCustomerByIdAsync(int Id)
        {
            var customer = await _unitofwork.Customers.GetByIdAsync(Id);
            if (customer == null)
                throw new Exception("Customer not found");
            return _mapper.Map<CustomerReadDto>(customer);

             
        }
        public async Task<IReadOnlyList<CustomerReadDto>> GetAllCustomersAsync()
        {
            var customers = await _unitofwork.Customers.GetAllAsync();
            var customerdto=_mapper.Map<IReadOnlyList<CustomerReadDto>>(customers);
            return customerdto;
        }
        public async Task DeleteCustomerAsync(int Id)
        {
            await _unitofwork.Customers.Delete(Id);
        }
        public async Task<CustomerReadDto> UpdateCustomerAsync(int Id, CustomerUpdateDto customerUpdateDto)
        {
            
            var customer = await _unitofwork.Customers.GetByIdAsync(Id);
            if (customer == null)
                throw new Exception("Customer not found");
           
            _mapper.Map(customerUpdateDto, customer);

            var customerdto = _mapper.Map<CustomerReadDto>(customer);
            return customerdto;

        }
        public async Task<IReadOnlyList<CustomerReadDto>> GetCustomersPagedAsync(int pageNumber, int pageSize)
        {
       var customer =  await _unitofwork.Customers.GetCustomersPagedAsync(pageNumber, pageSize);
            return _mapper.Map<IReadOnlyList<CustomerReadDto>>(customer);
        }
        public async Task<int> GetTotalCustomersCountAsync()
        {
            return await _unitofwork.Customers.GetTotalCustomersCountAsync();
        }

        public async Task SaveAllAsync()
        {
            await _unitofwork.SaveChangesAsync();
        }
    }
}
