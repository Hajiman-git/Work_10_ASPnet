using work_10.Dtos.Product;

namespace work_10.Dtos.StockReservation
{
    public class StockReservationDto
    {
        public int IdReservation { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public bool IsActive { get; set; }

        public ProductDto? Product { get; set; }
    }
}