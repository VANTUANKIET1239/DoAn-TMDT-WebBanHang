using CloudComputing.Conditions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class ChuotMayTinh : IEntitySPWithId
{
    public string Id { get; set; } = null!;
    [Required]
    [Display(Name = "Màu Sắc")]
    public string? Mau { get; set; }
    [Required]
    [Display(Name = "Kiểu Kết Nối")]
    public string? KieuKetNoi { get; set; }
    [Required]
    [Display(Name = "Đèn Led")]
    public string? DenLed { get; set; }
    [Required]
    [Display(Name = "Kết Nối")]
    public string? KetNoi { get; set; }
    [Required]
    [Display(Name = "Số Nút Bấm")]
    public int? SoNutBam { get; set; }
    [Required]
    [Display(Name = "Kích Thước")]
    public string? KichThuoc { get; set; }

    public string? IdDanhmuc { get; set; }
}
