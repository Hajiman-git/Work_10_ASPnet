using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.OrderItem
{
    public class UpdateOrderItemDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 100000000)]
        public decimal PriceAtTime { get; set; }
    }
}