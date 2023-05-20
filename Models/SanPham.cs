using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudComputing.Models;

public partial class SanPham
{
    public string IdSp { get; set; } = null!;

    public string IdDm { get; set; } = null!;
    [Required]
    public string? TenSanPham { get; set; }
    [Required]
    public string? ThuongHieu { get; set; }
    [Required]
    public string? BaoHanh { get; set; }
    [Required]
    public string? KhoiLuong { get; set; }
    [Required]
    public int? Gia { get; set; }
    [BindNever]
    public bool? TrangThai { get; set; }
    [Required]
    public string? MoTa { get; set; }
    [NotMapped]
    public IFormFile uploadfile { get; set; }
}
