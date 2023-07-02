using CloudComputing.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using CloudComputing.ViewModels;

namespace CloudComputing
{
    public static class ChucNangChung
    {
        public static void ThemSP<Tchitiet>(DbbhContext _db, SanPhamViewModel<Tchitiet> loaisp, List<IFormFile> hinhanh) where Tchitiet : class
        {
            if (hinhanh.Count > 0)
            {
                for (int i = 0; i < hinhanh.Count; i++)
                {
                    HinhAnhSp ha = new HinhAnhSp();
                    if (hinhanh[i].Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            hinhanh[i].CopyTo(memoryStream);
                            ha.HinhAnh = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                        ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                    }
                    var hanh = _db.HinhAnhSps.ToList();
                    ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                    ha.IdSp = loaisp.SanPham.IdSp;
                    _db.HinhAnhSps.Add(ha);
                    _db.SaveChanges();
                }
            }

            loaisp.SanPham.TrangThai = true;
            _db.SanPhams.Add(loaisp.SanPham);
            _db.SaveChanges();
            _db.Set<Tchitiet>().Add(loaisp.ChiTiet);
            _db.SaveChanges();
        }
        public static string chuyendoitiente(int? tien)
        {
            return tien?.ToString("#,##0") + "đ";
        }
        public static string chuyendoitieude(string title)
        {
            return title.Substring(0, title.Length < 50 ? title.Length : 50) + "...";
        }
        public static string SHA256_ENCRYPT(string matkhau)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(matkhau);
            string hashedString = "";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    sb.Append(hashedBytes[i].ToString("x2"));
                }

                hashedString = sb.ToString();

            }
            return hashedString;
        }
        public static void SendEmail(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("kietvan.31201024409@st.ueh.edu.vn", "0906889483");
            var mailMessage = new MailMessage("kietvan.31201024409@st.ueh.edu.vn", to, subject, body);
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }
      /*  public static List<SanPham> SPtheoDM(DbbhContext _db, string tenbang)
        {
            var toanbosp = _db.SanPhams.Where(x => x.TrangThai == true).ToList();
            var danhmuc = _db.DanhMucs.Where(x => x.State == true && x.TenBang.Trim() == tenbang).FirstOrDefault();
            List<HinhAnhSp> hinhanh = _db.HinhAnhSps.ToList();
            List<SanPham> bp = toanbosp
                .Where(x => x.IdDm.Trim() == danhmuc.Id.Trim())
                .Join(hinhanh, banphim => banphim.IdSp.Trim(), ha => ha.IdSp.Trim(), (banphim, ha) => new SanPham()
                {
                    IdSp = banphim.IdSp,
                    IdDm = banphim.IdDm,
                    Gia = banphim.Gia,
                    TenSanPham = banphim.TenSanPham,
                    HinhAnh = ha.HinhAnh,
                    ThuongHieu = banphim.ThuongHieu,
                    BaoHanh = banphim.BaoHanh,
                    KhoiLuong = banphim.KhoiLuong,
                    TrangThai = banphim.TrangThai,
                    MoTa = banphim.MoTa,
                    uploadfile = banphim.uploadfile,
                })
                .GroupBy(x => x.IdSp)
                .Select(x => x.FirstOrDefault(new SanPham()))
                .ToList();
            return bp;
        }*/
        public static List<SanPham> SPtheoDM2(DbbhContext _db, string tenbang)
        {
            List<SanPham> bp = _db.SanPhams
                .Where(sp => sp.TrangThai == true)
                .Join(_db.DanhMucs
                    .Where(dm => dm.State == true && dm.TenBang.Trim() == tenbang),
                    sp => sp.IdDm.Trim(),
                    dm => dm.Id.Trim(),
                    (sp, dm) => new { SanPham = sp, DanhMuc = dm })
                .Join(_db.HinhAnhSps,
                    spdm => spdm.SanPham.IdSp.Trim(),
                    ha => ha.IdSp.Trim(),
                    (spdm, ha) => new { spdm.SanPham, spdm.DanhMuc, HinhAnh = ha })
                .GroupBy(x => x.SanPham.IdSp)
                .Select(g => new SanPham
                {
                    IdSp = g.Key,
                    IdDm = g.First().DanhMuc.Id,
                    Gia = g.First().SanPham.Gia,
                    TenSanPham = g.First().SanPham.TenSanPham,
                    HinhAnh = g.First().HinhAnh.HinhAnh,
                    ThuongHieu = g.First().SanPham.ThuongHieu,
                    BaoHanh = g.First().SanPham.BaoHanh,
                    KhoiLuong = g.First().SanPham.KhoiLuong,
                    TrangThai = g.First().SanPham.TrangThai,
                    MoTa = g.First().SanPham.MoTa,
                    uploadfile = g.First().SanPham.uploadfile
                })
                .ToList();
            return bp;
        }
       
        public static List<SanPham> ToanBoSP(DbbhContext _db)
        {
            var toanbosp = _db.SanPhams.Where(x => x.TrangThai == true).ToList();
            var hinhanhsp = _db.HinhAnhSps.ToList();
            List<SanPham> bp = toanbosp.Join(hinhanhsp, sp => sp.IdSp.Trim(), ha => ha.IdSp.Trim(), (sp, ha) => new {sanpham = sp,hinhanh = ha})
            .GroupBy(x => x.sanpham.IdSp)
            .Select(x => new SanPham()
            {
                IdSp = x.Key,
                IdDm = x.First().sanpham.IdDm,
                Gia = x.First().sanpham.Gia,
                TenSanPham = x.First().sanpham.TenSanPham,
                HinhAnh = x.First().hinhanh.HinhAnh,
                ThuongHieu = x.First().sanpham.ThuongHieu,
                BaoHanh = x.First().sanpham.BaoHanh,
                KhoiLuong = x.First().sanpham.KhoiLuong,
                TrangThai = x.First().sanpham.TrangThai,
                MoTa = x.First().sanpham.MoTa,
                uploadfile = x.First().sanpham.uploadfile,
            })
            .ToList();
            return bp;
        }
        public static string vietmail(List<DetailHoaDon> dthd, HoaDon hoa, string ten)

        {
            string htmlBody = $@"<h2>Xác nhận đơn hàng</h2>
    <p>Xin chào,</p>
    <p>Cảm ơn bạn đã đặt hàng. Dưới đây là thông tin chi tiết về đơn hàng của bạn:</p>
    <hr/>
    <h4>Thông Tin Khách Hàng</h4>
    <p>Họ Tên: {ten} </p>
    <p>Số điện thoại: {hoa.Sdt}</p>
    <p>Địa chỉ giao:{hoa.DiaChi}  </p>

    <h4>Thông Tin Đơn Hàng</h4>
    <p>Mã đơn hàng: {hoa.Id} </p>
    <p>Ngày đặt hàng: {hoa.TimeStamp.ToString()}</p>
    <p>Tổng tiền: {hoa.Total}</p>
    <p>Phương thức thanh toán: {hoa.Payment}</p>
    <hr/>
    <table style=""text-align:center;padding:10px;border:2px solid black"">
        <tr>
            <th>Sản phẩm</th>
            <th>Số lượng</th>
            <th>Giá</th>
        </tr>";

            foreach (DetailHoaDon product in dthd)
            {
                htmlBody += $@"<tr>
                        <td>{product.TenSp}</td>
                        <td>{product.SoLuong}</td>
                        <td>{product.DonGia * product.SoLuong} VND</td>
                    </tr>";
            }

            htmlBody += @"</table>
    <p>Cảm ơn bạn đã mua hàng từ chúng tôi!</p>";
            return htmlBody;
        }

        public static string guimaillaymk(string randomnumber)

        {
            string htmlBody = $@"<h2>Xác nhận Email đổi mật khẩu</h2>
    <p>Xin chào,</p>
    <p>Cảm ơn bạn đã sử dụng dịch vụ. Dưới đây là mã xác nhận đổi mật khẩu:</p>
    <hr/>
    <h3>{randomnumber}</h3>
    <hr/>
    <p>Cảm ơn bạn rất nhiều!</p>";
            return htmlBody;
        }
    }


}
