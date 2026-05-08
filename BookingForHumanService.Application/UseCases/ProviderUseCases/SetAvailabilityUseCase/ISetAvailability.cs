using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.ProviderUseCases.SetAvailabilityUseCase
{
    public interface ISetAvailability
    {

        Task ExecuteAsync(int providerId, bool isAvailable);


    }
}
