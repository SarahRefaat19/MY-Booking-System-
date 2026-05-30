using AutoMapper;
using BookingForHumanService.Application.DTOs.AddressDtos;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = unitOfWork.Customers;
            _addressRepository = unitOfWork.Addresses;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ReadAddressDto> AddAddressAsync(int customerId, AddAddressDto dto)
        {

            var add = new Address(
          customerId,
          dto.City,
          dto.Region,
          dto.Street,
          dto.HomeNumber
      );

            await _addressRepository.AddAsync(add);
            await _unitOfWork.SaveChangesAsync();

            var addresss = _mapper.Map<ReadAddressDto>(add);
            return addresss;
        }
        public async Task<IReadOnlyList<ReadAddressDto>> GetUserAddressesAsync(int customerId)
        {

            var addresses = await _addressRepository.GetByIdAsync(customerId);

            return _mapper.Map<IReadOnlyList<ReadAddressDto>>(addresses);
        }

        public async Task SetDefaultAddressAsync(int customerId, int addressId)
        {
            // get all addresses
            var addresses = await _addressRepository.GetByCustomerIdAsync(customerId);
            //check if find
            if (addresses == null || !addresses.Any())
                throw new Exception("No Addresses Found");
            //get this address 
            var thisaddress = addresses.FirstOrDefault(a => a.Id == addressId);
            // check if find
            if (thisaddress == null)
            {
                throw new Exception("This Address Not Found ");

            }
            // iteration on 
            foreach (var address in addresses)
            {
                if (address.Id == addressId)
                {
                    address.SetAsDefault();
                }
                else
                {
                    address.RemoveDefault();
                }
            }

            foreach (var address in addresses)
            {
                await _addressRepository.UpdateAsync(address);
            }

            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteAddressAsync(int customerId, int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address == null || address.CustomerId != customerId)
                throw new Exception("Address not found");

            bool wasDefault = address.IsDefault;

            bool result = await _addressRepository.DeleteAsync(address.Id);

            if (!result)
                throw new InvalidOperationException("Operation Failed");

            if (wasDefault)
            {
                var remainingAddresses = await _addressRepository.GetByCustomerIdAsync(customerId);

                var newDefault = remainingAddresses.FirstOrDefault();

                if (newDefault != null)
                {
                    newDefault.SetAsDefault();
                    await _addressRepository.UpdateAsync(newDefault);
                }
            }


            await _unitOfWork.SaveChangesAsync();

        }
    }
}

