using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
