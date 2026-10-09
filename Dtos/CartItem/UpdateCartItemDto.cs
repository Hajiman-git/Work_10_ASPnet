using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.CartItem
{
    public class UpdateCartItemDto
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}