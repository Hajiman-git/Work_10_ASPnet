using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Description { get; set; }

    public bool IsService { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<StockReservation> StockReservations { get; set; } = new List<StockReservation>();
}
