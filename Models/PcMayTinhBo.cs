using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class PcMayTinhBo
{
    public string Id { get; set; } = null!;
    [Required]
    [Display(Name = "Màu Sắc")]
    public string? MauSac { get; set; }
    [Required]
    [Display(Name = "CPU")]
    public string? Cpu { get; set; }
    [Required]
    [Display(Name = "RAM")]
    public string? Ram { get; set; }
    [Required]
    [Display(Name = "Lưu Trữ")]
    public string? LuuTru { get; set; }
    [Required]
    [Display(Name = "Hệ Điều Hành")]
    public string? HeDieuHanh { get; set; }
    [Required]
    [Display(Name = "Cổng Kết Nối")]
    public string? CongKetNoi { get; set; }

    public string? IdDanhmuc { get; set; }
}
