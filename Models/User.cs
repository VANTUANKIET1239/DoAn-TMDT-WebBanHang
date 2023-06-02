using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CloudComputing.Models;

public partial class User
{
    public string Id { get; set; } = null!;
    [Display(Name = "Họ Tên")]
    public string Ten { get; set; } = null!;
    [Display(Name = "Số Điện Thoại")]
    public string Sdt { get; set; } = null!;

    [MinLength(6,ErrorMessage = "Mật khẩu phải dài hơn 6 kí tự")]
    [Display(Name = "Mật Khẩu")]
    public string PassWord { get; set; } = null!;

    [Display(Name = "Địa Chỉ")]
    public string? DiaChi { get; set; }

    public bool? State { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public User(string id, string ten, string sdt, string passWord, string? diaChi, bool? state, string? email)
    {
        Id = id;
        Ten = ten;
        Sdt = sdt;
        PassWord = passWord;
        DiaChi = diaChi;
        State = state;
        Email = email;
    }
    public User() {
        Id = "";
        Ten = "";
        Sdt = "";
        PassWord = "";
        DiaChi = "";
        State = true;
        Email = "";
    }
}
