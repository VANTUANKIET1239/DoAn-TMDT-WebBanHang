using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class TaiNghe
{
    public string Id { get; set; } = null!;

    public string? Mau { get; set; }

    public string? Kieu { get; set; }

    public string? KetNoi { get; set; }

    public string? Microphone { get; set; }

    public string? TanSoPhanHoi { get; set; }

    public string? TroKhang { get; set; }

    public string? IdDanhmuc { get; set; }
}
