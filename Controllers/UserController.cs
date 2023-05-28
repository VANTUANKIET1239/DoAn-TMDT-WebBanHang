using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CloudComputing.Controllers
{
    public class UserController : Controller
    {
        private readonly DbbhContext _db;

        public UserController(DbbhContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult DangNhap(string? checkthanhtoan)
        {
            ViewBag.check = checkthanhtoan;
            return View();
        }
        [HttpPost]
       
        public IActionResult DangNhap(string Email, string PassWord,string? checkthanhtoan)
        {
         
                if (ModelState.IsValid)
                {
                var nguoidung = _db.NguoiDungs.FirstOrDefault(x => x.State == true && x.Email.Trim() == Email.Trim());
                
                if (nguoidung == null) {    
                    ModelState.AddModelError("Email", "Email không đúng");
                }                
                     
                else if (!nguoidung.PassWord.Trim().Equals(ChucNangChung.SHA256_ENCRYPT(PassWord).Trim()))
                {
                    ModelState.AddModelError("PassWord", "Sai mật khẩu");
                }
                else
                {
                    var giohang = _db.DetailCarts.Where(x => x.IdUser.Trim() == nguoidung.Id).ToList();
                    int slcarthientai = giohang.Count;
                    if(HttpContext.Session.TryGetValue("Cart",out byte[] value))
                    {
                        int kiet = 0;
                        Dictionary<string, DetailCart>? sptronggio = JsonSerializer.Deserialize<Dictionary<string,DetailCart>>(HttpContext.Session.GetString("Cart") ?? "") ?? new Dictionary<string, DetailCart>();
                        foreach (KeyValuePair<string, DetailCart> keyValuePair in sptronggio)
                        {
                            for (int i = 0; i < giohang.Count; i++)
                            {
                                kiet = 0;
                                if (giohang[i].IdSp.Trim() == keyValuePair.Key.Trim())
                                {
                                    ++kiet;
                                    giohang[i].SoLuong += Convert.ToByte(Convert.ToInt32(giohang[i].SoLuong) + Convert.ToInt32(keyValuePair.Value.SoLuong));
                                    _db.DetailCarts.Update(giohang[i]);
                                    _db.SaveChanges();
                                    break;
                                }


                            }
                            if (kiet != 1)
                            {
                                slcarthientai += 1;
                                DetailCart detailCart = new DetailCart(keyValuePair.Key, nguoidung.Id, keyValuePair.Value.SoLuong,true);
                                _db.DetailCarts.Add(detailCart);
                                _db.SaveChanges();
                            }
                        }
                        HttpContext.Session.Remove("Cart");

                    }

                    HttpContext.Session.SetInt32("giohang", slcarthientai);
                    HttpContext.Session.SetString("username", nguoidung.Ten);
                    HttpContext.Session.SetString("id", nguoidung.Id);
                 
                    TempData["success"] = "Đăng Nhập Thành Công";
                  
                    if ((checkthanhtoan != null) && nguoidung.Roles.Trim() == "NGUOIDUNG")
                    {

                        return RedirectToAction("Index", "ThanhToan");
                    }
                    else if(nguoidung.Roles.Trim() == "NGUOIDUNG")
                    {
                        return RedirectToAction("Index", "Home");
                       
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    
                }
            }
            return View();
        }
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("id");
            TempData["success"] = "Đăng xuất Thành Công";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DangKy()
        {
            var countnguoi = _db.NguoiDungs.Count();
            string id = "ND" + (countnguoi + 1).ToString("000");
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(NguoiDung nguoiDung)
        {
        
            if (ModelState.IsValid)
            {
                if (!nguoiDung.PassWord.Trim().Equals(nguoiDung.PassWordXN.Trim()))
                {
                    ModelState.AddModelError("PassWord", "Mật khẩu không khớp");
                    ModelState.AddModelError("PassWordXN", "Mật khẩu xác nhận không khớp");
                }
                else
                {
                    nguoiDung.PassWord = ChucNangChung.SHA256_ENCRYPT(nguoiDung.PassWord);
                    nguoiDung.State = true;
                    nguoiDung.Roles = "NGUOIDUNG";
                    _db.NguoiDungs.Add(nguoiDung);
                    _db.SaveChanges();
                    TempData["success"] = "Đăng Ký Thành Công";
                    return RedirectToAction("DangNhap", "User");
                }
            }
            return View(nguoiDung);
        }
        public IActionResult Details(string idnguoidung)
        {
            var user = _db.NguoiDungs.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(idnguoidung.Trim()));
            var diachi = _db.DiaChis.FirstOrDefault(x => x.IdNguoiDung.Trim().Equals(idnguoidung.Trim()) && x.MacDinh == true);
            if(user == null)
            {
                return NotFound();
            }
            NguoiDungDiaChi_ViewModel nguoiDungDiaChi_ViewModel = new NguoiDungDiaChi_ViewModel()
            {
                nguoiDung = user,
                diaChi = diachi
            };
            return View(nguoiDungDiaChi_ViewModel);
        }
        [HttpPost]
        public IActionResult Details(NguoiDung nguoiDung)
        {
            NguoiDungDiaChi_ViewModel nguoiDungDiaChi_ViewModel = new NguoiDungDiaChi_ViewModel();
            var diachi = _db.DiaChis.FirstOrDefault(x => x.IdNguoiDung.Trim().Equals(nguoiDung.Id.Trim()) && x.MacDinh == true);
            ModelState.Remove("nguoiDung.PassWordXN");
            if (ModelState.IsValid)
            {
                nguoiDung.State = true;
                nguoiDung.Roles = "NGUOIDUNG";
                _db.NguoiDungs.Update(nguoiDung);
                _db.SaveChanges();
                TempData["success"] = "Cật Nhật Thành Công";
                nguoiDungDiaChi_ViewModel = new NguoiDungDiaChi_ViewModel()
                {
                    nguoiDung = nguoiDung,
                    diaChi = diachi
                };
                return RedirectToAction("Details",new { idnguoidung = nguoiDung.Id});
            }
           
            return View(nguoiDungDiaChi_ViewModel);
        }
        public IActionResult DonHang(string idnguoidung)  
        {
            var donhang = _db.HoaDons.Where(x => x.IdUser.Trim() == idnguoidung).ToList();
            
            if(donhang == null)
            {
                return NotFound();
            }
            var spdonhang = _db.DetailHoaDons.ToList();
            var spvadonhang = donhang.Join(spdonhang, dh => dh.Id, sp => sp.IdHoaDon, (dh, sp) => new
            {
                iddonhang = dh.Id,
                idsanpham = sp.IdSp
            });
            List<HinhAnh_HoaDonViewModel> hinhanh = spvadonhang
                .GroupBy(x => x.iddonhang)
                .Select(x => x.Join(ChucNangChung.ToanBoSP(_db), spdh => spdh.idsanpham, sptb => sptb.IdSp, (spdh, sptb) => new 
                {
                    iddh = x.Key,
                    hinhanhsp = sptb.HinhAnh
                }))
                .Select(x => new HinhAnh_HoaDonViewModel()
                {
                    HinhAnh = x.Select(y => y.hinhanhsp).ToList(),
                    HoaDon = donhang.FirstOrDefault(z => z.Id == x.Select(x => x.iddh).FirstOrDefault())
                }).ToList();
            ViewBag.idnguoidung = idnguoidung;
            return View(hinhanh);
        }
        public IActionResult ChiTietDonHang(string iddonhang)
        {
            string? idnguoidung = HttpContext.Session.GetString("id");
            // sửa lại phần hóa đơn là id địa chỉ, tạm thời để như vậy 
            string? ten = HttpContext.Session.GetString("username");
            var donhang = _db.HoaDons.FirstOrDefault(x => x.Id == iddonhang);
            var chitietdonhang = _db.DetailHoaDons.Where(x => x.IdHoaDon == iddonhang).ToList();
            var chitietspdonhang = chitietdonhang.Join(ChucNangChung.ToanBoSP(_db),ct => ct.IdSp, sptb => sptb.IdSp, (ct,sptb) => new
            {              
                thuonghieu = sptb.ThuongHieu,
                hinhanhsp = sptb.HinhAnh,
            }).ToList();
            ViewBag.CHITIET = chitietdonhang;
            ViewBag.hinhanh = chitietspdonhang.Select(x => x.hinhanhsp).ToList();
            ViewBag.thuonghieu = chitietspdonhang.Select(x => x.thuonghieu).ToList();
            ViewBag.HOADON = donhang;
            ViewBag.idnguoidung = idnguoidung;
            ViewBag.tennguoidung = ten;
            return View();
        }
        public IActionResult DiaChi(string idnguoidung,string iddiachi)
        {
            List<DiaChi> diachi = _db.DiaChis.Where(x => x.IdNguoiDung.Trim() == idnguoidung.Trim()).OrderByDescending(x => x.MacDinh).ToList();
            if(diachi == null)
            {
                return NotFound();
            }
            DiaChi diaChic = new DiaChi()
            {
                IdDiachi= "DC" + (diachi.Count + 1).ToString("000"),
                IdNguoiDung = idnguoidung
            };
            DiaChis_diachiViewModel dia = new DiaChis_diachiViewModel()
            {
                diaChis = diachi,
                diaChi = iddiachi == null ? diaChic : _db.DiaChis.FirstOrDefault(x => x.IdDiachi.Trim() == iddiachi.Trim())
            };
            ViewBag.IDND = idnguoidung;
            ViewBag.iddc = iddiachi;
            return View(dia);
        }
        
    }
}
