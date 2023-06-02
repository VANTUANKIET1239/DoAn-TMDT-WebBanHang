using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class Laptop
{
    public string Id { get; set; } = null!;
    [Required]
    [Display(Name = "Series Laptop")]
    public string? SeriesLaptop { get; set; }
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
    [Display(Name = "Nhân Đồ Họa")]
    public string? ChipDoHoa { get; set; }
    [Required]
    [Display(Name = "Màn Hình")]
    public string? ManHinh { get; set; }
    [Required]
    [Display(Name = "Lưu Trữ")]
    public string? LuuTru { get; set; }
    [Required]
    [Display(Name = "Cổng Kết Nối")]
    public string? CongKetNoi { get; set; }
    [Required]
    [Display(Name = "Loại Bàn Phím")]
    public string? BanPhim { get; set; }
    [Required]
    [Display(Name = "Hệ Điều Hành")]
    public string? HeDieuHanh { get; set; }
    [Required]
    [Display(Name = "Pin")]
    public string? Pin { get; set; }

    public string? IdDanhmuc { get; set; }
}
