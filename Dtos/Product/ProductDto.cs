using work_10.Dtos.Category;

namespace work_10.Dtos.Product
{
    public class ProductDto
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public bool IsService { get; set; }
        public string? ImageUrl { get; set; }

        public CategoryDto? Category { get; set; }
    }
}