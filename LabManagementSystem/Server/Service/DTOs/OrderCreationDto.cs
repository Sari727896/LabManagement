

namespace Service.DTOs
{
    public class OrderCreationDto
    {
        public OrderDto OrderDto { get; set; }
        public int CustomerId { get; set; }
        public int DeviceId { get; set; }

    }
}
