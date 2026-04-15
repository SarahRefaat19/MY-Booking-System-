using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;
using BookingForHumanService.Domain.ValueObjects.ProviderValueObjects;

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
        public async Task<ReadProviderDto> UpdateProfile(UpdateProviderDto providerupdatedto) {
            //Get
            var provider =  await _unitOfWork.Providers.GetByIdAsync(providerupdatedto.Id);

            //check

            if (provider == null)
            {
                throw new Exception("Provider Not Found");
            }

            //Update
            var nameVO = ProviderName.Create(providerupdatedto.Name);
            var emailVO = ProviderEmail.Create(providerupdatedto.Email);
            var phoneVO = ProviderPhone.Create(providerupdatedto.Phone);

            provider.UpdateProfile(nameVO, emailVO, phoneVO);                

            //save

            await _unitOfWork.SaveChangesAsync();

            //map
            return _mapper.Map<ReadProviderDto>(provider);

        }
    }
}
