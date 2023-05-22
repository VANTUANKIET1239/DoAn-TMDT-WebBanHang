using CloudComputing.Models;
using System.Security.Cryptography;
using System.Text;

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
    }
    
}
