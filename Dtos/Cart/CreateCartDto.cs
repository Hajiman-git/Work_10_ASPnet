using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Cart
{
    public class CreateCartDto
    {
        [Required]
        public int CustomerId { get; set; }
    }
}