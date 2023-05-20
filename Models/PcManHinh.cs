using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class PcManHinh
{
    public string Id { get; set; } = null!;
    [Required]
    public string? KichThuoc { get; set; }
    [Required]
    public string? DoPhanGiai { get; set; }
    [Required]
    public string? TamNen { get; set; }
    [Required]
    public string? KieuManHinh { get; set; }
    [Required]
    public string? CongXuatHinh { get; set; }
    [Required]
    public string? TanSoQuet { get; set; }
    [Required]
    public string? GocNhin { get; set; }
    [Required]
    public string? BeMat { get; set; }

    public string? IdDanhmuc { get; set; }
}
