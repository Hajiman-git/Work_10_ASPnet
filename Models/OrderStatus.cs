using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class OrderStatus
{
    public int IdStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
