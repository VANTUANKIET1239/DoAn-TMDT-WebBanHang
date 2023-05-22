using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace CloudComputing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbbhContext _db;

        public HomeController(ILogger<HomeController> logger, DbbhContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var danhmuc = _db.DanhMucs.ToList();
            HttpContext.Session.SetString("danhmuc", JsonSerializer.Serialize(danhmuc));
            ToanBoSP_DanhMuc toanBoSP_DanhMuc = new ToanBoSP_DanhMuc()
            {
                banphim = ChucNangChung.SPtheoDM(_db, "BANPHIM").Take(6).ToList(),
                chuot = ChucNangChung.SPtheoDM(_db, "CHUOT").Take(6).ToList(),
                tainghe = ChucNangChung.SPtheoDM(_db, "TAINGHE").Take(6).ToList(),
                pc = ChucNangChung.SPtheoDM(_db, "PCMAYTINHBO").Take(6).ToList(),
                manhinh = ChucNangChung.SPtheoDM(_db, "PCMANHINH").Take(6).ToList(),
                laptop = ChucNangChung.SPtheoDM(_db, "LAPTOP").Take(6).ToList()
            };
            return View(toanBoSP_DanhMuc);
        }

      
    }
}