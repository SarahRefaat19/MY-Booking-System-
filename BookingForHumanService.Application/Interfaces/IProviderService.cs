using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.Interfaces
{
    public  interface IProviderService 
    {
        Task<ReadProviderDto> GetByIdAsync(int id);

        Task<IReadOnlyList<ReadProviderDto>> GetAllAsync();

        Task<ReadProviderDto> AddAsync(AddProviderDto dto);

        Task<ReadProviderDto> UpdateProviderAsync(int Id, UpdateProviderDto updateProviderDto);

        Task DeleteAsync(int id);

        public Task<IReadOnlyList<ReadProviderDto>> GetTopRatedAsync(int count);

        public Task<bool> IsAvailableAsync(int id);

        public Task SetAvailabilityAsync(int id, bool isAvailable);

    }
}
