using CloudComputing.Conditions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class PcManHinh : IEntitySPWithId
{
    public string Id { get; set; } = null!;
    [Required]
    [Display(Name = "Kích Thước")]
    public string? KichThuoc { get; set; }
    [Required]
    [Display(Name = "Độ Phân Giải")]
    public string? DoPhanGiai { get; set; }
    [Required]
    [Display(Name = "Tấm Nền")]
    public string? TamNen { get; set; }
    [Required]
    [Display(Name = "Kiểu Màn Hình")]
    public string? KieuManHinh { get; set; }
    [Required]
    [Display(Name = "Công Suất Hình")]
    public string? CongXuatHinh { get; set; }
    [Required]
    [Display(Name = "Tần Số Quét")]
    public string? TanSoQuet { get; set; }
    [Required]
    [Display(Name = "Góc Nhìn")]
    public string? GocNhin { get; set; }
    [Required]
    [Display(Name = "Bề Mặt")]
    public string? BeMat { get; set; }

    public string? IdDanhmuc { get; set; }
}
