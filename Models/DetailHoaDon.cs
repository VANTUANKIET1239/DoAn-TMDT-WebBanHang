using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComputing.Models;

public partial class DetailHoaDon
{
    public string IdHoaDon { get; set; } = null!;

    public string IdSp { get; set; } = null!;
    [Display(Name = "Tên Sản Phẩm")]
    public string? TenSp { get; set; }

    [Display(Name = "Số Lượng")]
    public byte SoLuong { get; set; }
    [Display(Name = "Đơn Ggiá")]
    public int DonGia { get; set; }

    public bool? State { get; set; }

    // nên để tổng tiền tính được ở đây
}
