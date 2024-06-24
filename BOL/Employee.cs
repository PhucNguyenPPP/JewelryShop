using System;
using System.Collections.Generic;

namespace BOL;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public string? UserName { get; set; }

    public byte[]? PasswordHash { get; set; }

    public string? EmployeeName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateTime Dob { get; set; }

    public string? AvatarImg { get; set; }

    public bool? Status { get; set; }

    public Guid? CounterId { get; set; }

    public Guid? RoleId { get; set; }

    public virtual Counter? Counter { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
