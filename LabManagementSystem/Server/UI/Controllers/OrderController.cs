using Microsoft.AspNetCore.Mvc;
using Service.Configurations;
using Service.DTOs;
using Service.Interfaces;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(ServiceManager serviceManager)
        {
            _orderService = serviceManager.Orders;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderCreationDto orderCreationDto)
        {
            if (orderCreationDto == null)
            {
                return BadRequest("Order data is required.");
            }

            var orderId = await _orderService.AddOrderAsync(orderCreationDto.OrderDto, orderCreationDto.CustomerId, orderCreationDto.DeviceId);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        [HttpPut("{id}/finalprice")]
        public async Task<IActionResult> UpdateFinalPrice(int id,[FromBody] decimal finalPrice)
        {
            var updated = await _orderService.UpdateFinalPriceAsync(id, finalPrice);
            if (!updated)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent(); 
        }
        [HttpGet("detailed")]
        public async Task<IActionResult> GetActiveOrderDetails()
        {
            var orderDetails = await _orderService.GetActiveOrderDetailsAsync();
            if (!orderDetails.Any())
            {
                return NotFound("No active orders found.");
            }
            return Ok(orderDetails);
        }
        [HttpPut("{id}/status/{statusId}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, int statusId)
        {
            var success = await _orderService.UpdateOrderStatusAsync(id, statusId);
            if (!success)
            {
                return BadRequest("Failed to update order status.");
            }
            return NoContent();
        }
        [HttpGet("monthly-orders/{year}")]
        public async Task<IActionResult> GetMonthlyOrders(int year)
        {
            var monthlyOrders = await _orderService.GetMonthlyOrderCounts(year);
            if (monthlyOrders == null || monthlyOrders.Count == 0)
            {
                return NotFound("No order data found for the given year.");
            }
            return Ok(monthlyOrders);
        }

        [HttpGet("price-comparison")]
        public async Task<IActionResult> GetAveragePricesByDeviceType()
        {
            try
            {
                var priceComparisons = await _orderService.GetAveragePricesByDeviceTypeAsync();
                if (priceComparisons == null || !priceComparisons.Any())
                    return NotFound("No price data available.");

                return Ok(priceComparisons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
