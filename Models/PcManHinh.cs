using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class PcManHinh
{
    public string Id { get; set; } = null!;

    public string? KichThuoc { get; set; }

    public string? DoPhanGiai { get; set; }

    public string? TamNen { get; set; }

    public string? KieuManHinh { get; set; }

    public string? CongXuatHinh { get; set; }

    public string? TanSoQuet { get; set; }

    public string? GocNhin { get; set; }

    public string? BeMat { get; set; }

    public string? IdDanhmuc { get; set; }
}
