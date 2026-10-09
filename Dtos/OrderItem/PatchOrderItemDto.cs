using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.OrderItem
{
    public class PatchOrderItemDto
    {
        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }

        [Range(0, 100000000)]
        public decimal? PriceAtTime { get; set; }
    }
}