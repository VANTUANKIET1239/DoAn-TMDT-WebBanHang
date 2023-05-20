using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class HoaDon
{
    public string Id { get; set; } = null!;

    public string IdUser { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Payment { get; set; }

    public int ShipFee { get; set; }

    public int Total { get; set; }

    public bool? State { get; set; }

    public string? Ghichu { get; set; }
}
