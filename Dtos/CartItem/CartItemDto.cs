using work_10.Dtos.Product;

namespace work_10.Dtos.CartItem
{
    public class CartItemDto
    {
        public int IdCartItem { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }

        public ProductDto? Product { get; set; }
    }
}