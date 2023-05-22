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
        public IActionResult ThemSP_CHUOT([FromForm] List<IFormFile> hinhanh,Chuot_SanPhamViewModel chuot_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            
            if (ModelState.IsValid)
            {
                
                                
                    if (hinhanh.Count > 0)
                    {
                        for (int i = 0; i < hinhanh.Count; i++)
                        {
                            HinhAnhSp ha = new HinhAnhSp();
                            if (hinhanh[i].Length > 0)
                            {
                                using (var memoryStream = new MemoryStream())
                                {                                  
                                    hinhanh[i].CopyTo(memoryStream);
                                   ha.HinhAnh = memoryStream.ToArray();                                  
                                }
                            }
                            else
                            {
                                string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                                ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                            }
                            var hanh = _db.HinhAnhSps.ToList();
                            ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                            ha.IdSp = chuot_SanPhamViewModel.SanPham.IdSp;
                            _db.HinhAnhSps.Add(ha);
                            _db.SaveChanges();
                        }
                    }
                         
                chuot_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(chuot_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.ChuotMayTinhs.Add(chuot_SanPhamViewModel.ChiTiet);
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
        public IActionResult ThemSP_BANPHIM([FromForm] List<IFormFile> hinhanh, Banphim_SanPhamViewModel banphim_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                
                    if (hinhanh.Count > 0)
                    {
                        for (int i = 0; i < hinhanh.Count; i++)
                        {
                            HinhAnhSp ha = new HinhAnhSp();
                            if (hinhanh[i].Length > 0)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    hinhanh[i].CopyTo(memoryStream);
                                    ha.HinhAnh = memoryStream.ToArray();
                                }
                            }
                            else
                            {
                                string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                                ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                            }
                            var hanh = _db.HinhAnhSps.ToList();
                            ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                            ha.IdSp = banphim_SanPhamViewModel.SanPham.IdSp;
                            _db.HinhAnhSps.Add(ha);
                            _db.SaveChanges();
                        }
                    }
                
                banphim_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(banphim_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.BanPhims.Add(banphim_SanPhamViewModel.ChiTiet);
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
        public IActionResult ThemSP_PCMANHINH([FromForm] List<IFormFile> hinhanh, PCManhinh_SanPhamViewModel pCManhinh_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                if (hinhanh.Count > 0)
                {
                    for (int i = 0; i < hinhanh.Count; i++)
                    {
                        HinhAnhSp ha = new HinhAnhSp();
                        if (hinhanh[i].Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                hinhanh[i].CopyTo(memoryStream);
                                ha.HinhAnh = memoryStream.ToArray();
                            }
                        }
                        else
                        {
                            string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                            ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                        }
                        var hanh = _db.HinhAnhSps.ToList();
                        ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                        ha.IdSp = pCManhinh_SanPhamViewModel.SanPham.IdSp;
                        _db.HinhAnhSps.Add(ha);
                        _db.SaveChanges();
                    }
                }
                pCManhinh_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(pCManhinh_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.PcManHinhs.Add(pCManhinh_SanPhamViewModel.ChiTiet);
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
        public IActionResult ThemSP_PCMAYTINHBO([FromForm] List<IFormFile> hinhanh, PCMaytinhbo_SanPhamViewModel pCMaytinhbo_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                if (hinhanh.Count > 0)
                {
                    for (int i = 0; i < hinhanh.Count; i++)
                    {
                        HinhAnhSp ha = new HinhAnhSp();
                        if (hinhanh[i].Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                hinhanh[i].CopyTo(memoryStream);
                                ha.HinhAnh = memoryStream.ToArray();
                            }
                        }
                        else
                        {
                            string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                            ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                        }
                        var hanh = _db.HinhAnhSps.ToList();
                        ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                        ha.IdSp = pCMaytinhbo_SanPhamViewModel.SanPham.IdSp;
                        _db.HinhAnhSps.Add(ha);
                        _db.SaveChanges();
                    }
                }
                pCMaytinhbo_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(pCMaytinhbo_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.PcMayTinhBos.Add(pCMaytinhbo_SanPhamViewModel.ChiTiet);
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
        public IActionResult ThemSP_TAINGHE([FromForm] List<IFormFile> hinhanh, Tainghe_SanPhamViewModel tainghe_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                if (hinhanh.Count > 0)
                {
                    for (int i = 0; i < hinhanh.Count; i++)
                     {
                        HinhAnhSp ha = new HinhAnhSp();
                        if (hinhanh[i].Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                hinhanh[i].CopyTo(memoryStream);
                                ha.HinhAnh = memoryStream.ToArray();
                            }
                        }
                        else
                        {
                            string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                            ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                        }
                        var hanh = _db.HinhAnhSps.ToList();
                        ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                        ha.IdSp = tainghe_SanPhamViewModel.SanPham.IdSp;
                        _db.HinhAnhSps.Add(ha);
                        _db.SaveChanges();
                    }
                }
                tainghe_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(tainghe_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.TaiNghes.Add(tainghe_SanPhamViewModel.ChiTiet);
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
        public IActionResult ThemSP_LAPTOP([FromForm] List<IFormFile> hinhanh, Laptop_SanPhamViewModel latop_SanPhamViewModel)
        {
            ModelState.Remove("Sanpham.uploadfile");
            if (ModelState.IsValid)
            {
                if (hinhanh.Count > 0)
                {
                    for (int i = 0; i < hinhanh.Count; i++)
                    {
                        HinhAnhSp ha = new HinhAnhSp();
                        if (hinhanh[i].Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                hinhanh[i].CopyTo(memoryStream);
                                ha.HinhAnh = memoryStream.ToArray();
                            }
                        }
                        else
                        {
                            string filepath = "C:\\Users\\hungs\\Desktop\\Cloud\\wwwroot\\default - image.jpg";
                            ha.HinhAnh = System.IO.File.ReadAllBytes(filepath);
                        }
                        var hanh = _db.HinhAnhSps.ToList();
                        ha.IdHinhanh = "HA" + (hanh.Count + 1).ToString("000");
                        ha.IdSp = latop_SanPhamViewModel.SanPham.IdSp;
                        _db.HinhAnhSps.Add(ha);
                        _db.SaveChanges();
                    }
                }
                latop_SanPhamViewModel.SanPham.TrangThai = true;
                _db.SanPhams.Add(latop_SanPhamViewModel.SanPham);
                _db.SaveChanges();
                _db.Laptops.Add(latop_SanPhamViewModel.ChiTiet);
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
