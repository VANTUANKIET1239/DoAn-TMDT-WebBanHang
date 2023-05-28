using CloudComputing.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace CloudComputing
{
    public static class ChucNangChung
    {
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
        public static List<SanPham> SPtheoDM(DbbhContext _db, string tenbang)
        {
            var toanbosp = _db.SanPhams.Where(x => x.TrangThai == true).ToList();
            List<DanhMuc> danhmuc = _db.DanhMucs.Where(x => x.State == true).ToList();
            List<HinhAnhSp> hinhanh = _db.HinhAnhSps.ToList();
            List<SanPham> bp = toanbosp
                .Where(x => x.IdDm.Trim() == danhmuc.FirstOrDefault(y => y.TenBang.Trim() == tenbang, new DanhMuc()).Id.Trim())
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
        }
        public static List<SanPham> ToanBoSP(DbbhContext _db)
        {
            var toanbosp = _db.SanPhams.Where(x => x.TrangThai == true).ToList();
            var hinhanhsp = _db.HinhAnhSps.ToList();
            List<SanPham> bp = toanbosp.Join(hinhanhsp, sp => sp.IdSp.Trim(), ha => ha.IdSp.Trim(), (sp, ha) => new SanPham()
            {
                IdSp = sp.IdSp,
                IdDm = sp.IdDm,
                Gia = sp.Gia,
                TenSanPham = sp.TenSanPham,
                HinhAnh = ha.HinhAnh,
                ThuongHieu = sp.ThuongHieu,
                BaoHanh = sp.BaoHanh,
                KhoiLuong = sp.KhoiLuong,
                TrangThai = sp.TrangThai,
                MoTa = sp.MoTa,
                uploadfile = sp.uploadfile,
            })
            .GroupBy(x => x.IdSp)
            .Select(x => x.FirstOrDefault(new SanPham()))
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
    }


}
