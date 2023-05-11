using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class DanhMuc
{
    public string Id { get; set; } = null!;

    public string? TenDm { get; set; }

    public bool? State { get; set; }

    public string? TenBang { get; set; }
}
