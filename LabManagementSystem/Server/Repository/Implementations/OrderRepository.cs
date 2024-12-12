using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;


namespace Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LabContext _context;

        public OrderRepository(LabContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .Where(o => o.OrderId == orderId && (o.IsActive ?? false) && !(o.IsDeleted ?? false))
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Order order)
        {
            var receivedStatus = await _context.Statuses
                .FirstOrDefaultAsync(s => s.StatusName == "נכנס");

            if (receivedStatus != null)
            {
                order.StatusTblId = receivedStatus.StatusId;
            }
            else
            {
                throw new Exception("Received status not found in the database.");
            }

            _context.Orders.Add(order);
            await SaveAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            if (order.IsActive == true && order.IsDeleted == false)
            {
                _context.Orders.Update(order);
                await SaveAsync();
            }
            else
            {
                throw new Exception("Attempted to update a record that is not active or is deleted.");
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.CustomerTbl)
                .Include(o => o.StatusTbl)
                .Include(o => o.DeviceTbl)
                    .ThenInclude(d => d.DeviceTypeTbl)
                .Where(o => o.IsActive == true && o.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<(int Month, int Count)>> GetMonthlyOrderCounts(int year)
        {
            var data = await _context.Orders
                .Where(o => o.DateCreated.Value.Year == year && o.IsActive == true && o.IsDeleted == false)
                .GroupBy(o => new { Year = o.DateCreated.Value.Year, Month = o.DateCreated.Value.Month })
                .Select(group => new
                {
                    Month = group.Key.Month,
                    Count = group.Count()
                })
                .OrderBy(o => o.Month)
                .ToListAsync();

            return data.Select(x => (x.Month, x.Count)).ToList();
        }

        public async Task<List<(string DeviceType, decimal AverageInitialPrice, decimal AverageFinalPrice)>> GetAveragePricesByDeviceTypeAsync()
        {
            var results = await _context.Orders
                .Where(o => o.IsActive == true && o.IsDeleted == false)
                .Include(o => o.DeviceTbl)
                    .ThenInclude(d => d.DeviceTypeTbl)
                .GroupBy(o => o.DeviceTbl.DeviceTypeTbl.TypeName)
                .Select(group => new
                {
                    DeviceType = group.Key,
                    AverageInitialPrice = group.Average(o => o.EstimatedPrice ?? 0),
                    AverageFinalPrice = group.Average(o => o.FinalPrice ?? 0)
                })
                .ToListAsync();

            return results.Select(x => (x.DeviceType, x.AverageInitialPrice, x.AverageFinalPrice)).ToList();
        }

    }
}
