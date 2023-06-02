using CloudComputing.Models;

namespace CloudComputing.ViewModels
{
    public class SanPhamViewModel<Tchitiet>
    {
        public Tchitiet ChiTiet { get; set; }
        public SanPham SanPham { get; set; }
    }
}
