
using BookingForHumanService.Application.DTOs.AddressDtos;
using BookingForHumanService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingForHumanService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase 
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }


        [Authorize(Roles = "Customer")]

        [HttpPost("{customerId}")]
        public async Task<ActionResult<ReadAddressDto>> AddAddress(int customerId, [FromBody] AddAddressDto dto)
        {
            _logger.LogInformation("Adding address for CustomerId: {CustomerId}", customerId);

            var result = await _addressService.AddAddressAsync(customerId, dto);

            _logger.LogInformation("Address added successfully for CustomerId: {CustomerId}", customerId);

            return Ok(result);
        }
        [Authorize(Roles = "Admin,Provider")]

        [HttpGet("{customerId}")]
        public async Task<ActionResult<IReadOnlyList<ReadAddressDto>>> GetUserAddresses(int customerId)
        {
            _logger.LogInformation("Fetching addresses for CustomerId: {CustomerId}", customerId);

            var result = await _addressService.GetUserAddressesAsync(customerId);

            _logger.LogInformation("Fetched {Count} addresses for CustomerId: {CustomerId}", result.Count, customerId);

            return Ok(result);
        }
        [Authorize(Roles = "Customer")]

        [HttpPut("{customerId}/default/{addressId}")]
        public async Task<IActionResult> SetDefaultAddress(int customerId, int addressId)
        {
            _logger.LogInformation("Setting default address. CustomerId: {CustomerId}, AddressId: {AddressId}", customerId, addressId);

            await _addressService.SetDefaultAddressAsync(customerId, addressId);

            _logger.LogInformation("Default address set successfully");

            return Ok("Default address updated successfully");
        }

        [Authorize(Roles = "Customer")]

        [HttpDelete("{customerId}/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int customerId, int addressId)
        {
            _logger.LogWarning("Deleting address. CustomerId: {CustomerId}, AddressId: {AddressId}", customerId, addressId);

            await _addressService.DeleteAddressAsync(customerId, addressId);

            _logger.LogInformation("Address deleted successfully");

            return Ok("Address deleted successfully");
        }
    }


}

