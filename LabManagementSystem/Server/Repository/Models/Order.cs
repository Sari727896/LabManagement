using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public Guid? Guid { get; set; }

    public int CustomerTblId { get; set; }

    public int DeviceTblId { get; set; }

    public int StatusTblId { get; set; }

    public decimal? EstimatedPrice { get; set; }

    public decimal? FinalPrice { get; set; }

    public string Comments { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual Customer CustomerTbl { get; set; }

    public virtual Device DeviceTbl { get; set; }

    public virtual Status StatusTbl { get; set; }
}
