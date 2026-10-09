using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class StockReservation
{
    public int IdReservation { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public int Quantity { get; set; }

    public DateTime ReservedAt { get; set; }

    public DateTime? ReleasedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
