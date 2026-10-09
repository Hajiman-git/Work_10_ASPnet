using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.OrderStatus
{
    public class UpdateOrderStatusDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string StatusName { get; set; } = null!;
    }
}