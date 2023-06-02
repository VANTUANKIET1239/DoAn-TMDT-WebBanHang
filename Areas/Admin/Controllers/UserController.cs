using CloudComputing.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputing.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UserController : Controller
    {
        private readonly DbbhContext _db;

        public UserController(DbbhContext db)
        {
            _db = db;
        }
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("id");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowKhachHang()
        {
            var khachhang = _db.NguoiDungs.Where(x => x.State == true).ToList();

            return View(khachhang);
        }



    }
}
