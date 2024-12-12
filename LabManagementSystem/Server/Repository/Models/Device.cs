using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Device
{
    public int DeviceId { get; set; }

    public Guid? Guid { get; set; }

    public int DeviceTypeTblId { get; set; }

    public string Model { get; set; }

    public string IssueDescription { get; set; }

    public string UnlockCode { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual DeviceType DeviceTypeTbl { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
