using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public Guid? Guid { get; set; }

    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Comments { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public int? IcountId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
