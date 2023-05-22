using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudComputing.Models;

public partial class NguoiDung
{
    public string Id { get; set; } = null!;
    [Required]
    [Display(Name = "Họ Tên")]
    public string Ten { get; set; } = null!;
    [Display(Name = "Số Điện Thoại")]
    public string? Sdt { get; set; }
    [MinLength(6, ErrorMessage = "Mật khẩu phải dài hơn 6 kí tự")]
    [Required(AllowEmptyStrings = false,ErrorMessage = "Mật khẩu cần được nhâp vào")]
    [Display(Name = "Mật Khẩu")]
    public string? PassWord { get; set; }

    [NotMapped]
    [MinLength(6, ErrorMessage = "Mật khẩu phải dài hơn 6 kí tự")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu cần được nhâp vào")]
    [Display(Name = "Xác Nhận Mật Khẩu")]
    public string? PassWordXN { get; set; }

    [Required]
    [Display(Name = "Địa Chỉ")]
    public string? DiaChi { get; set; }
    [BindNever]
    public bool? State { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email cần được nhập vào")]
    public string? Email { get; set; }
    [BindNever]
    public string? Roles { get; set; }
    [Required]
    [Display(Name = "Ngày Sinh")]
    public DateTime? NgaySinh { get; set; }
}
