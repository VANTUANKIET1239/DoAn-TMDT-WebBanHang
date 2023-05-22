using CloudComputing.Models;
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
        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(string Email, string PassWord)
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
                    HttpContext.Session.SetString("username", nguoidung.Ten);
                    HttpContext.Session.SetString("id", nguoidung.Id);
                    TempData["success"] = "Đăng Nhập Thành Công";
                  
                    if (nguoidung.Roles.Trim() == "NGUOIDUNG")
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
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult DonHang()
        {
            return View();
        }
        public IActionResult DiaChi()
        {
            return View();
        }
    }
}
