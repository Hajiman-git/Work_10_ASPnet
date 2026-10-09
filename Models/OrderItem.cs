using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class OrderItem
{
    public int IdOrderItem { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceAtTime { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
