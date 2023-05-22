using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class BanPhim
{
    public string Id { get; set; } = null!;
    [Required]
    public string? Mau { get; set; }
    [Required]
    public string? KetNoi { get; set; }
    [Required]
    public string? KichThuoc { get; set; }
    [Required]
    public string? LoaiBanPhim { get; set; }
    [Required]
    public string? Den { get; set; }

    public string? IdDanhmuc { get; set; }
}
