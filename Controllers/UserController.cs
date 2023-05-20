using CloudComputing.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult DangKy()
        {
            return View();
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
