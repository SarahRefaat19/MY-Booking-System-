using AutoMapper;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;
using BookingForHumanService.Domain.ValueObjects.ProviderValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.ProviderUseCases
{
    public class UpdateProfile
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UpdateProfile(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<ReadProviderDto> ExecuteAsync(UpdateProviderDto dto)
        {
            // 1️⃣ Get Provider
            var provider = await _unitOfWork.Providers.GetByIdAsync(dto.Id);

            if (provider == null)
                throw new Exception("Provider not found");

            // 2️⃣ Create Value Objects
            var nameVO = ProviderName.Create(dto.Name);
            var emailVO = ProviderEmail.Create(dto.Email);
            var phoneVO = ProviderPhone.Create(dto.Phone);

            // 3️⃣ Update Domain
            provider.UpdateProfile(nameVO, emailVO, phoneVO);

            // 4️⃣ Save
            await _unitOfWork.SaveChangesAsync();

            // 5️⃣ Map to DTO
            return _mapper.Map<ReadProviderDto>(provider);
        }
    }
}
