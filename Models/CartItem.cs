using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class CartItem
{
    public int IdCartItem { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime AddedDate { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
