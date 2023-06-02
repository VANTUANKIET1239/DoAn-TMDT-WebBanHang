using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class TaiNghe
{
    public string Id { get; set; } = null!;
    [Display(Name = "Màu Sắc")]
    public string? Mau { get; set; }
    [Display(Name = "Kiểu Tai Nghe")]
    public string? Kieu { get; set; }
    [Display(Name = "Kết Nối")]
    public string? KetNoi { get; set; }
    [Display(Name = "Microphone")]
    public string? Microphone { get; set; }
    [Display(Name = "Tần Số Phản Hồi")]
    public string? TanSoPhanHoi { get; set; }
    [Display(Name = "Trở Kháng")]
    public string? TroKhang { get; set; }

    public string? IdDanhmuc { get; set; }
}
