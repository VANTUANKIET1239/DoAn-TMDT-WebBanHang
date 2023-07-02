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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var danhmuc = _db.DanhMucs.ToList();
            HttpContext.Session.SetString("danhmuc", JsonSerializer.Serialize(danhmuc));
            ToanBoSP_DanhMuc toanBoSP_DanhMuc = new ToanBoSP_DanhMuc()
            {
                banphim = ChucNangChung.SPtheoDM2(_db, "BANPHIM").ToList(),
                chuot = ChucNangChung.SPtheoDM2(_db, "CHUOT").ToList(),
                tainghe = ChucNangChung.SPtheoDM2(_db, "TAINGHE").ToList(),
                pc = ChucNangChung.SPtheoDM2(_db, "PCMAYTINHBO").ToList(),
                manhinh = ChucNangChung.SPtheoDM2(_db, "PCMANHINH").ToList(),
                laptop = ChucNangChung.SPtheoDM2(_db, "LAPTOP").ToList()
            };
            sw.Stop();
            Console.WriteLine("Thời gian chạy: " + sw.Elapsed.TotalMilliseconds);
            return View(toanBoSP_DanhMuc);
        }
        


    }
}