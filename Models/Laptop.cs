using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class Laptop
{
    public string Id { get; set; } = null!;
    [Required]
    public string? SeriesLaptop { get; set; }
    [Required]
    public string? MauSac { get; set; }
    [Required]
    public string? Cpu { get; set; }
    [Required]
    public string? Ram { get; set; }
    [Required]
    public string? ChipDoHoa { get; set; }
    [Required]
    public string? ManHinh { get; set; }
    [Required]
    public string? LuuTru { get; set; }
    [Required]
    public string? CongKetNoi { get; set; }
    [Required]
    public string? BanPhim { get; set; }
    [Required]
    public string? HeDieuHanh { get; set; }
    [Required]
    public string? Pin { get; set; }

    public string? IdDanhmuc { get; set; }
}
