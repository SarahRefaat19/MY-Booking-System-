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
    public class AddressService :IAddressService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _AddressRepository;
        private readonly IMapper _mapper;
        public AddressService(ICustomerRepository customerRepository, IAddressRepository AddressRepository, IMapper mapper)
        { 
            _customerRepository = customerRepository;
            _AddressRepository = AddressRepository;
           _mapper =mapper;

        }

      public async  Task<ReadAddressDto> AddAddressAsync(int customerId, AddAddressDto dto)
        {

            var add = new Address(
          customerId,
          dto.City,
          dto.Region,
          dto.Street,
          dto.HomeNumber
      );

            await _AddressRepository.AddAsync(add);
            var addresss = _mapper.Map<ReadAddressDto>(add);
            return addresss;
        }
        public async Task<IReadOnlyList<ReadAddressDto>> GetUserAddressesAsync(int customerId)
        {
          
            var addresses = await _AddressRepository.GetByIdAsync(customerId);

            return _mapper.Map<IReadOnlyList<ReadAddressDto>>(addresses);
        }

        public async Task SetDefaultAddressAsync(int customerId, int addressId)
        {
            // get all addresses
            var addresses = await _AddressRepository.GetByUserIdAsync(customerId);
             //check if find
            if (addresses == null || !addresses.Any())
                throw new Exception("No Addresses Found");
            //get this address 
            var thisaddress = addresses.FirstOrDefault(a=>a.Id == addressId);
            // check if find
            if(thisaddress == null)
            {
                throw new Exception("This Address Not Found ");

            }
            // iteration on 
            foreach (var address in addresses)
            {
                if(address.Id == addressId)
                {
                    address.SetAsDefault();

                }
                else
                {
                    address.RemoveDefault();
                }
            }

            foreach(var address in addresses)
            {
                await _AddressRepository.UpdateAsync(address);

            }

        }
       
            public async Task DeleteAddressAsync(int customerId, int addressId)
        {
            var address = await _AddressRepository.GetByIdAsync(addressId);

            if (address == null || address.CustomerId != customerId)
                throw new Exception("Address not found");

            bool wasDefault = address.IsDefault;

            await _AddressRepository.Delete(address.Id);

            if (wasDefault)
            {
                var remainingAddresses = await _AddressRepository.GetByUserIdAsync(customerId);

                var newDefault = remainingAddresses.FirstOrDefault();

                if (newDefault != null)
                {
                    newDefault.SetAsDefault();
                    await _AddressRepository.UpdateAsync(newDefault);
                }
            }
        }
    }



    }

