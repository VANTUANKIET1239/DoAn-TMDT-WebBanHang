using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

namespace CloudComputing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {

        private readonly DbbhContext _db;

        public SanPhamController(DbbhContext db)
        {
            _db = db;
        }
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


        /* public static SanPhamViewModel<T> SetSP<T>(DbbhContext _db,SanPham sanPham) where T : class
        {
            var chitiet = _db.Set<T>().FirstOrDefault(x => x.GetType().GetProperties()[0].GetValue(x.GetType()).ToString() == sanPham.IdSp);

            SanPhamViewModel<T> spv = new SanPhamViewModel<T>() { ChiTiet = chitiet, SanPham = sanPham };

            return spv; 
        }
        public IActionResult Details(string id)
         {
             var sanpham = _db.SanPhams.Where(x => x.TrangThai == true && x.IdSp == id).FirstOrDefault();

             if(sanpham == null)
             {
                 return NotFound();
             }
             var dm = _db.DanhMucs.Where(x => x.Id == sanpham.IdDm).FirstOrDefault();
             var hinhanh = _db.HinhAnhSps.Where(x => x.IdSp == sanpham.IdSp).ToList();
             ViewBag.HA = hinhanh;
             ViewBag.danhmucID = dm.Id;
             ViewBag.idsp = sanpham.IdSp;
             switch (dm.TenBang)
             {
                 case "PCMAYTINHBO": 
                     SanPhamViewModel<PcMayTinhBo> spv = SetSP<PcMayTinhBo>(_db, sanpham); return View("Details",spv);

                 case "PCMANHINH":
                     SanPhamViewModel<PcManHinh> spv1 = SetSP<PcManHinh>(_db, sanpham); return View("Details", spv1);

                 case "TAINGHE":
                     SanPhamViewModel<TaiNghe> spv2 = SetSP<TaiNghe>(_db, sanpham); return View("Details", spv2);

                 case "BANPHIM":
                     SanPhamViewModel<BanPhim> spv3 = SetSP<BanPhim>(_db, sanpham); return View("Details", spv3);

                 case "CHUOT":
                     SanPhamViewModel<ChuotMayTinh> spv4 = SetSP<ChuotMayTinh>(_db, sanpham); return View("Details", spv4);

                 case "LAPTOP":
                     SanPhamViewModel<Laptop> spv5 = SetSP<Laptop>(_db, sanpham); return View("Details", spv5);

             }
             return View("ThemChonTrangThemSP");
         }*/
        public IActionResult ThemSP()
        {
            var dm = _db.DanhMucs.Where(x => x.State == true).ToList();
            return View(dm);
        }
        [HttpPost]
        public IActionResult ThemSP_CHUOT([FromForm] List<IFormFile> hinhanh,SanPhamViewModel<ChuotMayTinh> chuot_SanPhamViewModel)
        {
            
            ModelState.Remove("Sanpham.uploadfile");
            
            if (ModelState.IsValid)
            {
                ChucNangChung.ThemSP<ChuotMayTinh>(_db, chuot_SanPhamViewModel, hinhanh);
                TempData["success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
                string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");
                var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(chuot_SanPhamViewModel.SanPham.IdDm.Trim()));
                ViewBag.danhmucID = danhmucne.Id;
                ViewBag.danhmuc = danhmuctoanbo;
                ViewBag.idsp = idsp;
                
            }
            return View("CHUOT",chuot_SanPhamViewModel);
        }
        [HttpPost]
        public IActionResult ThemSP_BANPHIM([FromForm] List<IFormFile> hinhanh, SanPhamViewModel<BanPhim> banphim_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {

                ChucNangChung.ThemSP<BanPhim>(_db, banphim_SanPhamViewModel, hinhanh);
                TempData["success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
                string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");
                var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(banphim_SanPhamViewModel.SanPham.IdDm.Trim()));
                ViewBag.danhmucID = danhmucne.Id;
                ViewBag.danhmuc = danhmuctoanbo;
                ViewBag.idsp = idsp;
            }
            return View("BANPHIM", banphim_SanPhamViewModel);
        }
        [HttpPost]
        public IActionResult ThemSP_PCMANHINH([FromForm] List<IFormFile> hinhanh, SanPhamViewModel<PcManHinh> pCManhinh_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                ChucNangChung.ThemSP<PcManHinh>(_db, pCManhinh_SanPhamViewModel, hinhanh);
                TempData["success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
                string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");
                var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(pCManhinh_SanPhamViewModel.SanPham.IdDm.Trim()));
                ViewBag.danhmucID = danhmucne.Id;
                ViewBag.danhmuc = danhmuctoanbo;
                ViewBag.idsp = idsp;
            }
            return View("PCMANHINH", pCManhinh_SanPhamViewModel);
        }
        [HttpPost]
        public IActionResult ThemSP_PCMAYTINHBO([FromForm] List<IFormFile> hinhanh, SanPhamViewModel<PcMayTinhBo> pCMaytinhbo_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                ChucNangChung.ThemSP<PcMayTinhBo>(_db, pCMaytinhbo_SanPhamViewModel, hinhanh);
                TempData["success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
                string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");
                var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(pCMaytinhbo_SanPhamViewModel.SanPham.IdDm.Trim()));
                ViewBag.danhmucID = danhmucne.Id;
                ViewBag.danhmuc = danhmuctoanbo;
                ViewBag.idsp = idsp;
            }
            return View("PCMAYTINHBO", pCMaytinhbo_SanPhamViewModel);
        }
        [HttpPost]
        public IActionResult ThemSP_TAINGHE([FromForm] List<IFormFile> hinhanh, SanPhamViewModel<TaiNghe> tainghe_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                ChucNangChung.ThemSP<TaiNghe>(_db, tainghe_SanPhamViewModel, hinhanh);
                TempData["success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");

            }
            else
            {
                var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
                string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");
                var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(tainghe_SanPhamViewModel.SanPham.IdDm.Trim()));
                ViewBag.danhmucID = danhmucne.Id;
                ViewBag.danhmuc = danhmuctoanbo;
                ViewBag.idsp = idsp;
            }
            return View("TAINGHE", tainghe_SanPhamViewModel);
        }
        [HttpPost]
        public IActionResult ThemSP_LAPTOP([FromForm] List<IFormFile> hinhanh, SanPhamViewModel<Laptop> latop_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                ChucNangChung.ThemSP<Laptop>(_db, latop_SanPhamViewModel, hinhanh);
                TempData["success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
                string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");
                var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(latop_SanPhamViewModel.SanPham.IdDm.Trim()));
                ViewBag.danhmucID = danhmucne.Id;
                ViewBag.danhmuc = danhmuctoanbo;
                ViewBag.idsp = idsp;
            }
            return View("LAPTOP", latop_SanPhamViewModel);
        }
        public IActionResult ThemChonTrangThemSP([FromQuery] string danhmuc)
        {
            var danhmuctoanbo = _db.DanhMucs.Where(x => x.State == true).ToList();
            if (danhmuctoanbo == null)
            {
                return NotFound();
            }
            string idsp = "SP" + (_db.SanPhams.Count() + 1).ToString("000");             
            var danhmucne = danhmuctoanbo.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(danhmuc.Trim()));
            ViewBag.danhmucID = danhmucne.Id;
            ViewBag.danhmuc = danhmuctoanbo;
            ViewBag.idsp = idsp;
            switch (danhmucne.TenBang.Trim())
            {
                case "PCMAYTINHBO": return View("PCMAYTINHBO"); 
                case "PCMANHINH": return View("PCMANHINH"); 
                case "TAINGHE": return View("TAINGHE"); 
                case "BANPHIM": return View("BANPHIM");
                case "CHUOT": return View("CHUOT");
                case "LAPTOP": return View("LAPTOP");
            }
            return View("ThemChonTrangThemSP");
        }

    }

}
