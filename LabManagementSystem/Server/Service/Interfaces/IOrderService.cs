using Service.DTOs;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrderAsync(OrderDto orderDto, int customerId, int deviceId);
        Task<bool> UpdateFinalPriceAsync(int orderId, decimal finalPrice);
        Task<OrderDto?> GetOrderByIdAsync(int orderId);

        Task<bool> UpdateOrderStatusAsync(int orderId, int newStatusId);
        Task<List<OrderDetailsDto>> GetActiveOrderDetailsAsync();
        Task<List<MonthlyOrderCountDto>> GetMonthlyOrderCounts(int year);
        Task<List<PriceComparisonDto>> GetAveragePricesByDeviceTypeAsync();

    }
}
