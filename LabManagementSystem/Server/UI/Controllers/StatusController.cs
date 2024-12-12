using Microsoft.AspNetCore.Mvc;
using Service.Configurations;
using Service.Interfaces;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(ServiceManager serviceManager)
        {
            _statusService = serviceManager.Status;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _statusService.GetAllStatusesAsync();
            if (statuses == null || statuses.Count == 0)
            {
                return NotFound("No statuses found.");
            }
            return Ok(statuses);
        }
    }
}
