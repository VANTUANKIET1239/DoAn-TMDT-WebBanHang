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
       
        
        public IActionResult ThemSP()
        {
            var dm = _db.DanhMucs.Where(x => x.State == true).ToList();
            return View(dm);
        }
        [HttpPost]
        public IActionResult ThemSP_CHUOT(Chuot_SanPhamViewModel chuot_SanPhamViewModel)
        {
            ModelState.Remove("uploadfile");
            if (ModelState.IsValid)
            {
                if (chuot_SanPhamViewModel.SanPham.uploadfile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        chuot_SanPhamViewModel.SanPham.uploadfile.CopyTo(memoryStream);
                        chuot_SanPhamViewModel.SanPham.HinhAnh = memoryStream.ToArray();
                    }
                }
                else
                {
                    string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                    chuot_SanPhamViewModel.SanPham.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                }
                chuot_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(chuot_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.ChuotMayTinhs.Add(chuot_SanPhamViewModel.chuotMayTinh);
                _db.SaveChanges();
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
        public IActionResult ThemSP_BANPHIM(Banphim_SanPhamViewModel banphim_SanPhamViewModel)
        {
            ModelState.Remove("uploadfile");
            if (ModelState.IsValid)
            {
                if (banphim_SanPhamViewModel.SanPham.uploadfile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        banphim_SanPhamViewModel.SanPham.uploadfile.CopyTo(memoryStream);
                        banphim_SanPhamViewModel.SanPham.HinhAnh = memoryStream.ToArray();
                    }
                }
                else
                {
                    string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                    banphim_SanPhamViewModel.SanPham.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                }
                banphim_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(banphim_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.BanPhims.Add(banphim_SanPhamViewModel.banPhim);
                _db.SaveChanges();
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
        public IActionResult ThemSP_PCMANHINH(PCManhinh_SanPhamViewModel pCManhinh_SanPhamViewModel)
        {
            ModelState.Remove("uploadfile");
            if (ModelState.IsValid)
            {
                if (pCManhinh_SanPhamViewModel.SanPham.uploadfile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        pCManhinh_SanPhamViewModel.SanPham.uploadfile.CopyTo(memoryStream);
                        pCManhinh_SanPhamViewModel.SanPham.HinhAnh = memoryStream.ToArray();
                    }
                }
                else
                {
                    string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                    pCManhinh_SanPhamViewModel.SanPham.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                }
                pCManhinh_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(pCManhinh_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.PcManHinhs.Add(pCManhinh_SanPhamViewModel.pcManHinh);
                _db.SaveChanges();
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
        public IActionResult ThemSP_PCMAYTINHBO(PCMaytinhbo_SanPhamViewModel pCMaytinhbo_SanPhamViewModel)
        {
            ModelState.Remove("uploadfile");
            if (ModelState.IsValid)
            {
                if (pCMaytinhbo_SanPhamViewModel.SanPham.uploadfile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        pCMaytinhbo_SanPhamViewModel.SanPham.uploadfile.CopyTo(memoryStream);
                        pCMaytinhbo_SanPhamViewModel.SanPham.HinhAnh = memoryStream.ToArray();
                    }
                }
                else
                {
                    string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                    pCMaytinhbo_SanPhamViewModel.SanPham.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                }
                pCMaytinhbo_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(pCMaytinhbo_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.PcMayTinhBos.Add(pCMaytinhbo_SanPhamViewModel.pcMayTinhBo);
                _db.SaveChanges();
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
        public IActionResult ThemSP_TAINGHE(Tainghe_SanPhamViewModel tainghe_SanPhamViewModel)
        {
            ModelState.Remove("uploadfile");
            if (ModelState.IsValid)
            {
                if (tainghe_SanPhamViewModel.SanPham.uploadfile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        tainghe_SanPhamViewModel.SanPham.uploadfile.CopyTo(memoryStream);
                        tainghe_SanPhamViewModel.SanPham.HinhAnh = memoryStream.ToArray();
                    }
                }
                else
                {
                    string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                    tainghe_SanPhamViewModel.SanPham.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                }
                tainghe_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(tainghe_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.TaiNghes.Add(tainghe_SanPhamViewModel.taiNghe);
                _db.SaveChanges();
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
        public IActionResult ThemSP_LAPTOP(Laptop_SanPhamViewModel latop_SanPhamViewModel)
        {
            ModelState.Remove("uploadfile");
            if (ModelState.IsValid)
            {
                if (latop_SanPhamViewModel.SanPham.uploadfile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        latop_SanPhamViewModel.SanPham.uploadfile.CopyTo(memoryStream);
                        latop_SanPhamViewModel.SanPham.HinhAnh = memoryStream.ToArray();
                    }
                }
                else
                {
                    string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                    latop_SanPhamViewModel.SanPham.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                }
                latop_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(latop_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.Laptops.Add(latop_SanPhamViewModel.laptop);
                _db.SaveChanges();
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
            string idsp = "SP" + (_db.SanPhams.Where(x => x.TrangThai == true).Count() + 1).ToString("000");             
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
