using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public Guid? Guid { get; set; }

    public string StatusName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
