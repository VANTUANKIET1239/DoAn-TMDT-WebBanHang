using CloudComputing.Conditions;
using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
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

        public IActionResult DangNhap(string Email, string PassWord, string? checkthanhtoan)
        {

            if (ModelState.IsValid)
            {
                var nguoidung = _db.NguoiDungs.FirstOrDefault(x => x.State == true && x.Email.Trim() == Email.Trim());

                if (nguoidung == null)
                {
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
                    if (HttpContext.Session.TryGetValue("Cart", out byte[] value))
                    {
                        int kiet = 0;
                        Dictionary<string, DetailCart>? sptronggio = JsonSerializer.Deserialize<Dictionary<string, DetailCart>>(HttpContext.Session.GetString("Cart") ?? "") ?? new Dictionary<string, DetailCart>();
                        if (sptronggio != null)
                        {
                            List<DetailCart> spgiohangList = sptronggio.Values.ToList();
                            Dictionary<string, DetailCart> spgiongnhau = giohang.Intersect(spgiohangList, new DetailCartComparer()).ToDictionary(x => x.IdSp);
                            Dictionary<string, DetailCart> spkhacnhau = spgiohangList.Except(spgiongnhau.Values.ToList(), new DetailCartComparer()).ToDictionary(x => x.IdSp);

                            foreach (DetailCart dtc in giohang)
                            {
                                if (spgiongnhau.ContainsKey(dtc.IdSp))
                                {
                                    dtc.SoLuong += (byte)Convert.ToInt32(spgiongnhau[dtc.IdSp].SoLuong);
                                    _db.DetailCarts.Update(dtc);
                                    _db.SaveChanges();
                                }
                            }

                            foreach (KeyValuePair<string, DetailCart> dtc in spkhacnhau)
                            {
                                slcarthientai += 1;
                                DetailCart detailCart = new(dtc.Key, nguoidung.Id, dtc.Value.SoLuong, true);
                                _db.DetailCarts.Add(detailCart);
                                _db.SaveChanges();
                            }
                         

                        }                                     
                       
                    }

                    HttpContext.Session.Remove("Cart");
                    HttpContext.Session.SetInt32("giohang", slcarthientai);
                    HttpContext.Session.SetString("username", nguoidung.Ten);
                    HttpContext.Session.SetString("id", nguoidung.Id);

                    TempData["success"] = "Đăng Nhập Thành Công";

                    if ((checkthanhtoan != null) && nguoidung.Roles.Trim() == "NGUOIDUNG")
                    {

                        return RedirectToAction("Index", "ThanhToan");
                    }
                    else if (nguoidung.Roles.Trim() == "NGUOIDUNG")
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
            /*return PartialView("_layout");*/
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
            else
            {
                var countnguoi = _db.NguoiDungs.Count();
                string id = "ND" + (countnguoi + 1).ToString("000");
                ViewBag.ID = id;
            }
            return View(nguoiDung);
        }
        public IActionResult Details(string idnguoidung)
        {
            var user = _db.NguoiDungs.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(idnguoidung.Trim()));
            var diachi = _db.DiaChis.FirstOrDefault(x => x.IdNguoiDung.Trim().Equals(idnguoidung.Trim()) && x.MacDinh == true);
            if (user == null)
            {
                return NotFound();
            }
            if (diachi == null)
            {
                diachi = new DiaChi();
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
            var diachi = _db.DiaChis.FirstOrDefault(x => x.IdNguoiDung.Trim().Equals(nguoiDung.Id.Trim()) && x.MacDinh == true) ?? new DiaChi();
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
                return RedirectToAction("Details", new { idnguoidung = nguoiDung.Id });
            }

            return View(nguoiDungDiaChi_ViewModel);
        }
        public IActionResult DonHang(string idnguoidung, int pagesize = 6, int page = 1)
        {
            var donhang = _db.HoaDons.Where(x => x.IdUser.Trim() == idnguoidung).ToList();

            if (donhang == null)
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
                    HoaDon = donhang.FirstOrDefault(z => z.Id == x.Select(x => x.iddh).FirstOrDefault()) ?? new HoaDon()
                }).ToList();
            // phân trang 
            int totalCount = hinhanh.Count;
            int skip = (page - 1) * pagesize;
            var pagedItems = hinhanh.Skip(skip).Take(pagesize).ToList();


            var viewModel = new PagedViewModel<HinhAnh_HoaDonViewModel>
            {
                Items = pagedItems,
                PageIndex = page,
                PageSize = pagesize,
                TotalCount = totalCount
            };
            ViewBag.idnguoidung = idnguoidung;
            ViewBag.username = HttpContext.Session.GetString("username");
            return View(viewModel);
        }
        public IActionResult ChiTietDonHang(string iddonhang)
        {
            string? idnguoidung = HttpContext.Session.GetString("id");
            // sửa lại phần hóa đơn là id địa chỉ, tạm thời để như vậy 
            string? ten = HttpContext.Session.GetString("username");
            var donhang = _db.HoaDons.FirstOrDefault(x => x.Id == iddonhang);
            var chitietdonhang = _db.DetailHoaDons.Where(x => x.IdHoaDon == iddonhang).ToList();
            var chitietspdonhang = chitietdonhang.Join(ChucNangChung.ToanBoSP(_db), ct => ct.IdSp, sptb => sptb.IdSp, (ct, sptb) => new
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
        public IActionResult DiaChi(string idnguoidung, string iddiachi)
        {
            var diachidb = _db.DiaChis;
            List<DiaChi> diachi = diachidb.Where(x => x.IdNguoiDung.Trim() == idnguoidung.Trim()).OrderByDescending(x => x.MacDinh).ToList();
            if (diachi == null)
            {
                return NotFound();
            }
            DiaChi diaChic = new DiaChi()
            {
                IdDiachi = "DC" + (diachidb.Count() + 1).ToString("000"),
                IdNguoiDung = idnguoidung
            };
            // nếu mà ngdung bấm vào qly địa chi
            DiaChis_diachiViewModel dia = new DiaChis_diachiViewModel()
            {
                diaChis = diachi,
                diaChi = iddiachi == null ? diaChic : _db.DiaChis.FirstOrDefault(x => x.IdDiachi.Trim() == iddiachi.Trim()) ?? new DiaChi()
            };
            ViewBag.IDND = idnguoidung;
            ViewBag.iddc = iddiachi;
            ViewBag.username = HttpContext.Session.GetString("username");
            return View(dia);
        }
        [HttpGet]
        public IActionResult QuenMatKhau()
            {
            return View();
        }
        [HttpPost]
        public IActionResult QuenMatKhau(string email)
        {
            if (email == "") ModelState.AddModelError("email", "Email chưa được nhập");
            var checkemail = _db.NguoiDungs.Where(x => x.State == true && x.Email.Trim() == email.Trim()).FirstOrDefault();
            if (checkemail == null) ModelState.AddModelError("email", "Email không tồn tại");
            else
            {
                return RedirectToAction("XacNhanMa", "User",new {emailcheck = true, email = email});
            }
            return View();
        }
        [NonAction]
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int randumnumber  = random.Next(0,99999);
            return randumnumber.ToString();
        }
        [HttpGet]
        public IActionResult XacNhanMa(bool emailcheck, string email)
        {
            if (!emailcheck) return NotFound();
            string verificationCode = GenerateVerificationCode(); // Hàm này sinh mã xác nhận ngẫu nhiên
            DateTime expirationTime = DateTime.Now.AddMinutes(1);

            VerificationCode code = new VerificationCode
            {
                Code = verificationCode,
                ExpirationTime = expirationTime
            };
            ChucNangChung.SendEmail(email.Trim(), "[Mail xác nhận đổi mật khẩu]", ChucNangChung.guimaillaymk(verificationCode));
            ViewBag.emailxn = email;
            return View(code);
        }
        [HttpPost]
        public IActionResult XacNhanMa(string code, string maxacnhan, string emailxn)
        {
            if(maxacnhan.Trim() != code.Trim())
            {
                return NotFound();
            }
            return RedirectToAction("MatKhauMoi", "User", new {emailxn = emailxn});
        }
        [HttpGet]
        public IActionResult MatKhauMoi(string emailxn) 
        {         
            ViewBag.emailxn = emailxn;            
            return View(new Dictionary<string, string>() { { "", "" },});
        }

        [HttpGet]
        public IActionResult GuiLaiMa(bool emailcheck, string email)
        {
            return RedirectToAction("XacNhanMa", "User", new { emailcheck = emailcheck, email = email });
        }
        [HttpPost]
        public IActionResult MatKhauMoi(string matkhaucu, string matkhaumoi, string xacnhanmatkhaumoi, string emailxn)
        {
            Dictionary<string,string> validate = new Dictionary<string, string>();
           
            
            var nguoidung = _db.NguoiDungs.Where(x => x.State == true && x.Email.Trim() == emailxn.Trim()).FirstOrDefault();

            if (nguoidung?.PassWord != ChucNangChung.SHA256_ENCRYPT(matkhaucu)) 
            {
                validate.Add("saimkcu", "Mật khẩu cũ nhập không đúng");
                ViewBag.valid = "saimkcu";
            } 
            else if(matkhaumoi.Trim() != xacnhanmatkhaumoi.Trim())
            {
                validate.Add("matkhaukhongkhop", "Mật khẩu mới với xác nhận không khớp");
                ViewBag.valid = "matkhaukhongkhop";
            }
            else
            {
                nguoidung.PassWord = ChucNangChung.SHA256_ENCRYPT(matkhaumoi);
                _db.NguoiDungs.Update(nguoidung);
                _db.SaveChanges();
                return RedirectToAction("DangNhap", "User");
            }
            return View(validate);
        }


    }
}

