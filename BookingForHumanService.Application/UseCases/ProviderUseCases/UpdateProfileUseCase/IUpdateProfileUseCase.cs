using BookingForHumanService.Application.DTOs.ProviderDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Application.UseCases.ProviderUseCases.UpdateProfileUseCase
{
    public  interface IUpdateProfileUseCase
    {
        Task<ReadProviderDto> ExecuteAsync(UpdateProviderDto dto);

    }
}
