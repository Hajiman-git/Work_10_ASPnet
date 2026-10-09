using work_10.Dtos.Customer;

namespace work_10.Dtos.Cart
{
    public class CartDto
    {
        public int IdCart { get; set; }
        public int CustomerId { get; set; }
        public DateOnly CreatedDate { get; set; }

        public CustomerDto? Customer { get; set; }
    }
}