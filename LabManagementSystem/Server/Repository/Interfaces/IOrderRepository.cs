using Repository.Models;


namespace Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task UpdateAsync(Order order);
        Task<Order?> GetByIdAsync(int orderId);
        Task SaveAsync();
        Task<IEnumerable<Order>> GetActiveOrdersAsync();
        Task<List<( int Month, int Count)>> GetMonthlyOrderCounts(int year);
        Task<List<(string DeviceType, decimal AverageInitialPrice, decimal AverageFinalPrice)>> GetAveragePricesByDeviceTypeAsync();

    }
}
