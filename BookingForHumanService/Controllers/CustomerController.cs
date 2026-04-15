using BookingForHumanService.API.Response;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Application.UseCases.CustomerUseCases;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
namespace BookingForHumanService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly UpdateProfileUseCase _updateProfileUseCase;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, UpdateProfileUseCase updateProfileUseCase)
        {
            _logger = logger;

            _customerService = customerService;
            _updateProfileUseCase = updateProfileUseCase; 
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            _logger.LogInformation($"Getting Customer wih Id :{id}");
            var customer =  await _customerService.GetCustomerByIdAsync(id);
            if (customer==null)
            {
               _logger.LogWarning($" Customer With Id: {id} Is Not Found");
                return NotFound(ApiResponse<CustomerReadDto>.Fail("Customer not found"));
            }
            return Ok(ApiResponse<CustomerReadDto>.Ok(customer));

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<IActionResult> GetAllCustomersAsync()
        {
            _logger.LogInformation($"Getting All Customers ");
            var customers = await _customerService.GetAllCustomersAsync();


            return Ok(ApiResponse<IReadOnlyList<CustomerReadDto>>.Ok(customers));

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("pages")]

        public async Task<IActionResult> GetCustomersPagesAsync(int pagenumber=1, int pageSize=10)
        {
            var customers = await _customerService.GetCustomersPagedAsync(pagenumber, pageSize);
            var totalCount = await _customerService.GetTotalCustomersCountAsync();


            var Response = new ApiPagedResponse<IReadOnlyList<CustomerReadDto>>
            {
                Data = customers,
                PageNumber = pagenumber,
                PageSize = pageSize,
                TotalCount = totalCount,
            };

            return Ok(Response);


        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> CreateCustomerAsync(CustomerAddDto customerAddDto)
        {
            _logger.LogInformation($"Add Customer");
            var customer = await _customerService.CreateCustomerAsync(customerAddDto);


            return CreatedAtAction(nameof(GetCustomerByIdAsync),new {Id=customer.Id},ApiResponse<CustomerReadDto>.Ok(customer));

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int Id)
        {
            _logger.LogInformation($"Getting  Customer You Want to Delete  ");
            var customer = await _customerService.GetCustomerByIdAsync(Id);
            if(customer== null)
            {
                _logger.LogWarning($" Customer With Id: {Id} Is Not Found");
                return NotFound();
            }
             await _customerService.DeleteCustomerAsync(Id);

            return Ok();

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]

        public async Task<IActionResult> UpdateCustomerAsync(int id,CustomerUpdateDto customerUpdateDto)
        {
            _logger.LogInformation($"Getting  Customer You Want to Update  ");

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                _logger.LogWarning($" Customer With Id:{id} Is Not Found");
                return NotFound();
            }
          var  customerupdated = await _customerService.UpdateCustomerAsync(id, customerUpdateDto);

            return Ok(customerupdated);

        }
        [Authorize(Roles ="Customer")]
        [HttpPut("profile")]


        public async Task<IActionResult> UpdateProfileAsync( CustomerUpdateDto dto)
        {
        
            var customer = int.Parse(User.FindFirst("Id")!.Value);
            var updatedCustomer = await _updateProfileUseCase.UpdateProfile(dto);

            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }


    }
    }
