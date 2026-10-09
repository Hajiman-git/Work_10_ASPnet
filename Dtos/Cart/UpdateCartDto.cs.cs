using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Cart
{
    public class UpdateCartDto
    {
        [Required]
        public int CustomerId { get; set; }
    }
}