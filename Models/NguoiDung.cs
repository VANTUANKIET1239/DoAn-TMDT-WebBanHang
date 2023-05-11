using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class NguoiDung
{
    public string Id { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? PassWord { get; set; }

    public string? DiaChi { get; set; }

    public bool? State { get; set; }

    public string? Email { get; set; }

    public string? Roles { get; set; }
}
