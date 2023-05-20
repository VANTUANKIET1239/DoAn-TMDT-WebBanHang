using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

namespace CloudComputing.Controllers
{
   
    public class SanPhamController : Controller
    {

        private readonly DbbhContext _db;

        public SanPhamController(DbbhContext db)
        {
            _db = db;
        }
        // làm hiện toàn bộ sản phẩm
        public IActionResult Index([FromQuery] string danhmuc)
        {
            var dm = _db.DanhMucs.Where(x => x.State == true).ToList();
            if (danhmuc == null)
            {
                var mtb = _db.SanPhams.Where(x => x.TrangThai == true).ToList();
                ViewData["dm"] = dm;
                ViewData["iddmht"] = "";
                return View(mtb);
            }
            else
            {

                DanhMuc dmuc = dm.Where(x => x.State == true && x.Id.Trim().Equals(danhmuc.Trim())).FirstOrDefault();
                ViewData["iddmht"] = dmuc.Id;
                ViewData["dm"] = dm;
                var sp = _db.SanPhams.Where(x => x.TrangThai == true && x.IdDm.Trim().Equals(danhmuc.Trim())).ToList();
                return View(sp);
                
            }

        }
       
        
      
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult ToanBoSP()
        {
            return View();
        }
    }
    

}
