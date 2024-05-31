using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateTime? Dob { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? AvatarImg { get; set; }

    public Guid? EmployeeId { get; set; }

    public virtual ICollection<BuyBackOrder> BuyBackOrders { get; set; } = new List<BuyBackOrder>();

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
