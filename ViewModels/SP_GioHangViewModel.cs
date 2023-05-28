using CloudComputing.Models;

namespace CloudComputing.ViewModels
{
    public class SP_GioHangViewModel
    {
        public SanPham sanPham { get; set; }
        public byte? soluong { get; set; }
        public string idus { get; set; }

        public bool? check { get; set; }
    }
}
