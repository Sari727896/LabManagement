using Microsoft.AspNetCore.Mvc;
using Service.Configurations;
using Service.DTOs;
using Service.Interfaces;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ServiceManager serviceManager)
        {
            _customerService = serviceManager.Customers;
        }
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetCustomerByPhoneNumber(string phoneNumber)
        {
            var customer = await _customerService.GetCustomerByPhoneNumberAsync(phoneNumber);
            if (customer == null)
            {
                return NotFound($"Customer with phone number {phoneNumber} not found.");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto customerDto)
        {
            var existingCustomer = await _customerService.GetCustomerByPhoneNumberAsync(customerDto.PhoneNumber);
            if (existingCustomer != null)
            {
                return Conflict("Customer with this phone number already exists.");
            }

            var customerId = await _customerService.AddCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetCustomerByPhoneNumber), new { phoneNumber = customerDto.PhoneNumber }, customerId);
        }
    }
}
