using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class Cart
{
    public int IdCart { get; set; }

    public int CustomerId { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Customer Customer { get; set; } = null!;
}
