using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class DeviceType
{
    public int DeviceTypeId { get; set; }

    public Guid? Guid { get; set; }

    public string TypeName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
