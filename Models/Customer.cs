using System;
using System.Collections.Generic;

namespace work_10.Models;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int RoleId { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<StockReservation> StockReservations { get; set; } = new List<StockReservation>();
}
