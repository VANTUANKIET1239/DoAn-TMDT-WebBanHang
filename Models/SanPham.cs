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
    [Display(Name = "Tên Sản Phẩm")]
    public string? TenSanPham { get; set; }
    [NotMapped]
    public byte[]? HinhAnh { get; set; }
    [Required]
    [Display(Name = "Thương Hiệu")]
    public string? ThuongHieu { get; set; }
    [Required]
    [Display(Name = "Bảo Hành")]
    public string? BaoHanh { get; set; }
    [Required]
    [Display(Name = "Khối Lượng")]
    public string? KhoiLuong { get; set; }
    [Required]
    [Display(Name = "Đơn Gía")]
    public int? Gia { get; set; }
    [BindNever]
    public bool? TrangThai { get; set; }
    [Required]
    [Display(Name = "Mô Tả")]
    public string? MoTa { get; set; }

    [NotMapped]
    public List<IFormFile>? uploadfile { get; set; }

    public SanPham()
    {
        IdSp = "";
        IdDm = "";
        TenSanPham = "";
        HinhAnh = null;
        ThuongHieu = "";
        BaoHanh = "";
        KhoiLuong = "";
        Gia = 0;
        TrangThai = true;
        MoTa = "";
        this.uploadfile = null;
    }
}
