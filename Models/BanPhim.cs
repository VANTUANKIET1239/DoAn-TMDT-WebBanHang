using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class BanPhim
{
    public string Id { get; set; } = null!;
    [Required]
    [Display(Name = "Màu Sắc")]
    public string? Mau { get; set; }
    [Required]
    [Display(Name = "Kết Nối")]
    public string? KetNoi { get; set; }
    [Required]
    [Display(Name = "Kích Thước")]
    public string? KichThuoc { get; set; }
    [Required]
    [Display(Name = "Loại Bàn Phím")]
    public string? LoaiBanPhim { get; set; }
    [Required]
    [Display(Name = "Đèn Led")]
    public string? Den { get; set; }

    public string? IdDanhmuc { get; set; }
}
