

namespace Service.DTOs
{
    public class OrderDetailsDto : OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public string TypeName { get; set; } 
        public string IssueDescription { get; set; }
        public int? CustomerICountId { get; set; }

    }
}
