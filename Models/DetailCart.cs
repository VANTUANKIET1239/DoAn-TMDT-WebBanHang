using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class DetailCart
{
    public string IdUser { get; set; } = null!;

    public string IdSp { get; set; } = null!;

    public byte SoLuong { get; set; }
}
