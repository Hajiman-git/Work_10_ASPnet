using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string ProductName { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(10, 1000000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public bool IsService { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }
    }
}