using CloudComputing.Models;

namespace CloudComputing.ViewModels
{
    public class ThanhToanViewModel
    {
        public List<DetailHoaDon> detailHoaDons { get; set; }   
        public HoaDon HoaDon { get; set; }
        public DiaChi diaChi { get; set; }
        public string IDNguoiDung { get; set; }



        public ThanhToanViewModel(List<DetailHoaDon> detailHoaDons, HoaDon hoaDon,DiaChi diaChi, string IDNguoiDung)
        {
            this.detailHoaDons = detailHoaDons;
            HoaDon = hoaDon;            
            this.diaChi = diaChi;
            this.IDNguoiDung = IDNguoiDung;           
        }
        public ThanhToanViewModel()
        {
            this.detailHoaDons = new List<DetailHoaDon>();
            HoaDon = new HoaDon();
            this.diaChi = new DiaChi();
            this.IDNguoiDung = "";
        }
    }
}
