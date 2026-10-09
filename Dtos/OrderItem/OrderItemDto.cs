using work_10.Dtos.Product;

namespace work_10.Dtos.OrderItem
{
    public class OrderItemDto
    {
        public int IdOrderItem { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtTime { get; set; }

        public ProductDto? Product { get; set; }
    }
}