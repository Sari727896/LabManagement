using AutoMapper;
using Repository.Configurations;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public OrderService(RepositoryManager repositoryManager, IMapper mapper)
        {
            _orderRepository = repositoryManager.OrderRepository;
            _customerRepository = repositoryManager.CustomerRepository;
            _deviceRepository = repositoryManager.DeviceRepository;
            _statusRepository = repositoryManager.StatusRepository;
            _mapper = mapper;
        }

        public async Task<int> AddOrderAsync(OrderDto orderDto, int customerId, int deviceId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            var device = await _deviceRepository.GetByIdAsync(deviceId);

            if (customer == null || device == null)
            {
                throw new ArgumentException("Customer or Device not found in the database.");
            }

            var newOrder = _mapper.Map<Order>(orderDto);
            newOrder.CustomerTblId = customerId;
            newOrder.DeviceTblId = deviceId;
            newOrder.FinalPrice = null;

            await _orderRepository.AddAsync(newOrder);
            await _orderRepository.SaveAsync();
            return newOrder.OrderId;
        }

        public async Task<bool> UpdateFinalPriceAsync(int orderId, decimal finalPrice)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(orderId);
            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.FinalPrice = finalPrice;
            await _orderRepository.UpdateAsync(existingOrder);
            return true;
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDetailsDto>> GetActiveOrderDetailsAsync()
        {
            var orders = await _orderRepository.GetActiveOrdersAsync();
            return _mapper.Map<List<OrderDetailsDto>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, int newStatusId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return false; // Order not found
            }

            var status = await _statusRepository.GetStatusByIdAsync(newStatusId);
            if (status == null || !status.IsActive.GetValueOrDefault() || status.IsDeleted.GetValueOrDefault())
            {
                return false; // Invalid status
            }

            order.StatusTblId = newStatusId;
            await _orderRepository.UpdateAsync(order);
            return true;
        }
        public async Task<List<MonthlyOrderCountDto>> GetMonthlyOrderCounts(int year)
        {
            var results = await _orderRepository.GetMonthlyOrderCounts(year);
            return results.Select(r => new MonthlyOrderCountDto
            {
                Month = r.Month,
                OrderCount = r.Count
            }).ToList();
        }
        public async Task<List<PriceComparisonDto>> GetAveragePricesByDeviceTypeAsync()
        {
            var results = await _orderRepository.GetAveragePricesByDeviceTypeAsync();
            return results.Select(x => new PriceComparisonDto
            {
                DeviceType = x.DeviceType,
                AverageInitialPrice = x.AverageInitialPrice,
                AverageFinalPrice = x.AverageFinalPrice
            }).ToList();
        }
    }
}
