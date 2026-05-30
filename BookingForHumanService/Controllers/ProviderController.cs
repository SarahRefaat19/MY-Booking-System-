using BookingForHumanService.API.Response;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.DTOs.ProviderDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Application.Services;
using BookingForHumanService.Application.UseCases.BookingUseCases.AcceptBookingUseCase;
using BookingForHumanService.Application.UseCases.BookingUseCases.RejectBookingUseCase;
using BookingForHumanService.Application.UseCases.ProviderUseCases.SetAvailabilityUseCase;
using BookingForHumanService.Application.UseCases.ProviderUseCases.UpdateProfileUseCase;
using BookingForHumanService.Domain.Entities;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;


namespace BookingForHumanService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {

        private readonly ILogger<ProviderController> _logger;
        private readonly IProviderService _providerService;

        private readonly ISetAvailability _setAvailabilityUseCase;
        private readonly IUpdateProfileUseCase _updateProfileUseCase;


        public ProviderController(ILogger<ProviderController> logger, IProviderService providerService, ISetAvailability setAvailabilityUseCase, IUpdateProfileUseCase updateProfileUseCase)
        {
            _logger = logger;
            _providerService = providerService;

            _setAvailabilityUseCase = setAvailabilityUseCase;
            _updateProfileUseCase = updateProfileUseCase;
        }


        // CRUD
        //GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {

            _logger.LogInformation("Getting Provider By Id ");
            var provider = await _providerService.GetByIdAsync(id);

            if (provider == null)
            {
                _logger.LogWarning($" provider With Id: {id} Is Not Found");
                return NotFound(ApiResponse<ReadProviderDto>.Fail("ProviderNotFound"));
            }
            return Ok(provider);

        }


        //Create

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> CreateProviderAsync([FromBody] AddProviderDto addProviderDto)
        {
            _logger.LogInformation($"Add Provider");
            var provider = await _providerService.AddAsync(addProviderDto);


            return CreatedAtAction(nameof(GetByIdAsync), new { Id = provider.Id }, ApiResponse<ReadProviderDto>.Ok(provider));

        }

        [Authorize(Roles = "Admin,Provider")]

        //Update
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProviderAsync(int id, [FromBody] UpdateProviderDto updateProviderDto)
        {
            _logger.LogInformation($"Getting  Provider You Want to Update  ");

            var provider = await _providerService.GetByIdAsync(id);
            if (provider == null)
            {
                _logger.LogWarning($" Provider With Id:{id} Is Not Found");
                return NotFound();
            }
            var providerupdated = await _providerService.UpdateProviderAsync(id, updateProviderDto);

            return Ok(ApiResponse<ReadProviderDto>.Ok(providerupdated));

        }


        [Authorize(Roles = "Admin")]

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProviderAsync(int id, [FromBody] DeleteProviderDto deleteProviderDto)
        {
            _logger.LogInformation("Deleting provider ");
            if (id != deleteProviderDto.Id)
                return BadRequest("Id mismatch");

            await _providerService.DeleteAsync(id);


            return NoContent();
        }



        [Authorize(Roles = "Admin")]


        //GetAll
        [HttpGet("Get All Providers")]
        public async Task<IActionResult> GetAllProvidersAsync()
        {
            _logger.LogInformation("Getting All Providers ");
            var providers = await _providerService.GetAllAsync();
            return Ok(ApiResponse<IReadOnlyList<ReadProviderDto>>.Ok(providers));

        }


        // Business logic
        //  GetTopRated

        [HttpGet("Get Top Rated Providers")]
        public async Task<IActionResult> GetTopRatedProviders(int count)
        {
            _logger.LogInformation("Getting Top Rated Providers ");
            var providers = await _providerService.GetTopRatedAsync(count);
            return Ok(ApiResponse<IReadOnlyList<ReadProviderDto>>.Ok(providers));
        }

        [Authorize(Roles = "Provider")]

        //SetAvailability

        [HttpPut("{id}/availability")]
        public async Task<IActionResult> SetAvailabiltyAsync(int id, bool isAvailable)
        {
            _logger.LogInformation("Setting availability for provider {ProviderId} to {IsAvailable}", id, isAvailable);
            await _providerService.SetAvailabilityAsync(id, isAvailable);

            return Ok(isAvailable);

        }
    }
}

