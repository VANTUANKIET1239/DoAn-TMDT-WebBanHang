using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class HinhAnhSp
{
    public string IdHinhanh { get; set; } = null!;

    public string IdSp { get; set; } = null!;

    [BindNever]
    public byte[]? HinhAnh { get; set; } = null!;

    public HinhAnhSp()
    {
        IdHinhanh = "";
        IdSp = "";
        HinhAnh = null;
    }
}
