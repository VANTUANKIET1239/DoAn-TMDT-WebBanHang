using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class TaiNghe
{
    public string Id { get; set; } = null!;
    [Required]
    public string? Mau { get; set; }
    [Required]
    public string? Kieu { get; set; }
    [Required]
    public string? KetNoi { get; set; }
    [Required]
    public string? Microphone { get; set; }
    [Required]
    public string? TanSoPhanHoi { get; set; }
    [Required]
    public string? TroKhang { get; set; }

    public string? IdDanhmuc { get; set; }
}
