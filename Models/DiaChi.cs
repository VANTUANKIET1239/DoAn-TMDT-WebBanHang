using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class DiaChi
{
    public string IdDiachi { get; set; } = null!;

    [Display(Name = "Địa Chỉ Cụ Thể")]
    public string? DiaChiCuThe { get; set; } = "";
    [Display(Name = "Tỉnh/Thành Phố")]
    public string? ThanhPho { get; set; } = "";
    [Display(Name = "Quận/Huyện")]
    public string? Quan { get; set; } = "";

    [Display(Name = "Phường/Xã")]
    public string? Phuong { get; set; } = "";

    public string? IdNguoiDung { get; set; } = "";


    public bool MacDinh { get; set; } = true;
    [Display(Name = "Email")]
    public string? Email { get; set; } = "";
    [Display(Name = "Số Điện Thoại")]
    public string? Sdt { get; set; } = "";
    [Display(Name = "Họ Tên")]
    public string? HoTen { get; set; } = "";

    /*public DiaChi()
    {
        IdDiachi = "";
        DiaChiCuThe = "";
        ThanhPho = "";
        Quan = "";
        Phuong = "";
        IdNguoiDung = "";
        MacDinh = false;
        Email = "";
        Sdt = "";
        HoTen = "";
    }*/
}
