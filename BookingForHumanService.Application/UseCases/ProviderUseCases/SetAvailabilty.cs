using AutoMapper;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.ProviderUseCases
{
    public class SetAvailabilty
    {
        private IUnitOfWork _unitOfWork;

        public SetAvailabilty( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task ExecuteAsync(int providerId, bool isAvailable)
        {
            // 1️⃣ Get provider
            var provider = await _unitOfWork.Providers.GetByIdAsync(providerId);

            if (provider == null)
                throw new Exception("Provider not found");

            // 2️⃣ Update
            provider.UpdateAvailability(isAvailable);

            // 3️⃣ Save
            await _unitOfWork.Providers.UpdateAsync(provider);
            await _unitOfWork.SaveChangesAsync();

        }
}
