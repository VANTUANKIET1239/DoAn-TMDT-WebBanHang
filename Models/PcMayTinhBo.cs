using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class PcMayTinhBo
{
    public string Id { get; set; } = null!;

    public string? MauSac { get; set; }

    public string? Cpu { get; set; }

    public string? Ram { get; set; }

    public string? LuuTru { get; set; }

    public string? HeDieuHanh { get; set; }

    public string? CongKetNoi { get; set; }

    public string? IdDanhmuc { get; set; }
}
