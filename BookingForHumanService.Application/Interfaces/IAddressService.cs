using BookingForHumanService.Application.DTOs.AddressDtos;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Interfaces
{
    public interface  IAddressService
    {

        Task<ReadAddressDto> AddAddressAsync(int customerId, AddAddressDto address);
        Task<IReadOnlyList<ReadAddressDto>> GetUserAddressesAsync(int customerId);
        Task SetDefaultAddressAsync(int customerId, int addressId);
        Task DeleteAddressAsync(int customerId, int addressId);


    }
}
