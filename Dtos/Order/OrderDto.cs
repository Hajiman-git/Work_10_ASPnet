using work_10.Dtos.Customer;
using work_10.Dtos.OrderStatus;

namespace work_10.Dtos.Order
{
    public class OrderDto
    {
        public int IdOrder { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Comment { get; set; }

        public CustomerDto? Customer { get; set; }
        public OrderStatusDto? Status { get; set; }
    }
}