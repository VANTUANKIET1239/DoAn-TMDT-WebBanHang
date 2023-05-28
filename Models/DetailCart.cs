using System;
using System.Collections.Generic;

namespace CloudComputing.Models;

public partial class DetailCart
{
    public string IdSp { get; set; } = null!;

    public string IdUser { get; set; } = null!;

    public byte? SoLuong { get; set; }

    public bool? State { get; set; }

    public DetailCart(string idSp, string idUser, byte? soLuong, bool? state)
    {
        IdSp = idSp;
        IdUser = idUser;
        SoLuong = soLuong;
        State = state;
    }
}
