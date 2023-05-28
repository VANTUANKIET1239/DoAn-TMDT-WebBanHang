using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class DiaChi
{
    public string IdDiachi { get; set; } = null!;

    public string? DiaChiCuThe { get; set; } = "";

    public string? ThanhPho { get; set; } = "";

    public string? Quan { get; set; } = "";

    public string? Phuong { get; set; } = "";

    public string? IdNguoiDung { get; set; } = "";


    public bool MacDinh { get; set; } = true;

    public string? Email { get; set; } = "";

    public string? Sdt { get; set; } = "";

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
