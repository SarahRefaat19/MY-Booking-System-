using AutoMapper;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using Castle.Core.Logging;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProviderService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IMapper mapper, ILogger<ProviderService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public async Task<IReadOnlyList<ReadProviderDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all providers");

            var providers = await _unitOfWork.Providers.GetAllAsync();

            return _mapper.Map<IReadOnlyList<ReadProviderDto>>(providers);
        }

        public async Task<ReadProviderDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting Provider By Id {id}", id);

            var provider = await _unitOfWork.Providers.GetByIdAsync(id);
            if (provider == null)
            {
                throw new InvalidOperationException("Provider Not Found");
            }


            return _mapper.Map<ReadProviderDto>(provider);
        }


        public async Task<ReadProviderDto> UpdateProviderAsync(int id, UpdateProviderDto dto)
        {
            _logger.LogInformation("Updating provider {ProviderId}", dto.Id);

            var provider = await _unitOfWork.Providers.GetByIdAsync(dto.Id);

            if (provider == null)
                throw new InvalidOperationException("Provider Not Found");

            _mapper.Map(dto, provider);

            await _unitOfWork.SaveChangesAsync();
            var updated = _mapper.Map<ReadProviderDto>(provider);

            return updated;
        }



        public async Task<ReadProviderDto> AddAsync(AddProviderDto dto)
        {
            _logger.LogInformation("Creating new provider");

            var provider = _mapper.Map<Provider>(dto);

            await _unitOfWork.Providers.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadProviderDto>(provider);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting provider {ProviderId}", id);

            var provider = await _unitOfWork.Providers.GetByIdAsync(id);

            if (provider == null)
                throw new InvalidOperationException("Provider Not Found");

            await _unitOfWork.Providers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ReadProviderDto>> GetTopRatedAsync(int count)
        {
            _logger.LogInformation("Getting Provider Top Rated");

            var providers = await _unitOfWork.Providers.GetAllAsync();

            var result = providers
                .OrderByDescending(p => p.Rating)
                .Take(count)
                .ToList();

            return _mapper.Map<IReadOnlyList<ReadProviderDto>>(result);

        }

        public async Task<bool> IsAvailableAsync(int id)
        {
            _logger.LogInformation("Getting Provider By Id {id} to Check Availability", id);

            var provider = await _unitOfWork.Providers.GetByIdAsync(id);

            if (provider == null)
            {
                _logger.LogWarning("Availability check failed, provider not found {ProviderId}", id);
                throw new InvalidOperationException("Not Found");
            }

            _logger.LogInformation("Provider availability {Status}", provider.IsAvailable);

            return provider.IsAvailable;
        }

        public async Task SetAvailabilityAsync(int id, bool isAvailable)
        {
            _logger.LogInformation("Getting Provider By Id {id} to Set Availability", id);

            var provider = await _unitOfWork.Providers.GetByIdAsync(id);



            if (provider == null)
                throw new InvalidOperationException("Not Found");

            _logger.LogInformation("Provider availability {Status}", provider.IsAvailable);


            provider.UpdateAvailability(isAvailable);

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task<IReadOnlyList<ReadProviderDto>> GetByCityAsync(string city)
        {
            _logger.LogInformation("Getting providers by city {City}", city);

            var providers = await _unitOfWork.Providers.GetByCityAsync(city);

            return _mapper.Map<IReadOnlyList<ReadProviderDto>>(providers);
        }

        public async Task<IReadOnlyList<ReadProviderDto>> GetByServiceTypeAsync(string serviceType)
        {
            _logger.LogInformation("Getting providers by service type {ServiceType}", serviceType);

            var providers = await _unitOfWork.Providers.GetByServiceAsync(serviceType);


            return _mapper.Map<IReadOnlyList<ReadProviderDto>>(providers);
        }
    }
}
