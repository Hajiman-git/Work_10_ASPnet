using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.StockReservation
{
    public class ReserveStockDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}