using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class ChuotMayTinh
{
    public string Id { get; set; } = null!;
    [Required]
    public string? Mau { get; set; }
    [Required]
    public string? KieuKetNoi { get; set; }
    [Required]
    public string? DenLed { get; set; }
    [Required]
    public string? KetNoi { get; set; }
    [Required]
    public int? SoNutBam { get; set; }
    [Required]
    public string? KichThuoc { get; set; }

    public string? IdDanhmuc { get; set; }
}
