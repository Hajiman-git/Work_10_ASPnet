using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        [Range(0, 100000000)]
        public decimal TotalAmount { get; set; }

        [StringLength(200)]
        public string? DeliveryAddress { get; set; }

        [StringLength(300)]
        public string? Comment { get; set; }
    }
}