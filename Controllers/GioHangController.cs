using CloudComputing.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputing.Controllers
{
    public class GioHangController : Controller
    {
        private readonly DbbhContext _db;

        public GioHangController(DbbhContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult ThanhToan()
        {

            return View();
        }
    }
}
