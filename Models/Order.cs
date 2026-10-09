using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public int StatusId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? Comment { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus Status { get; set; } = null!;
}
