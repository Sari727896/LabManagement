using Microsoft.AspNetCore.Mvc;
using Service.Configurations;
using Service.DTOs;
using Service.Interfaces;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(ServiceManager serviceManager)
        {
            _deviceService = serviceManager.Devices;
        }
        [HttpGet("{model}")]
        public async Task<IActionResult> GetDeviceByModel(string model)
        {
            var device = await _deviceService.GetDeviceByModelAsync(model);
            if (device == null)
            {
                return NotFound($"Device with model '{model}' not found.");
            }
            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] DeviceDto deviceDto)
        {
            var deviceId = await _deviceService.AddDeviceAsync(deviceDto);
            return CreatedAtAction(nameof(GetDeviceByModel), new { model = deviceDto.Model }, deviceId);
        }
        [HttpGet("device-types")]
        public async Task<IActionResult> GetDeviceTypes()
        {
            var deviceTypes = await _deviceService.GetDeviceTypesAsync();
            return Ok(deviceTypes);
        }
    }
}
