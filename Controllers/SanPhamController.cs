﻿using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
    
        
        public IActionResult Details(string id)
        {
            var danhmuc = _db.DanhMucs.ToList();
            List<SanPham> spthem = new List<SanPham>();
            var sanpham = _db.SanPhams.FirstOrDefault(x => x.TrangThai == true && x.IdSp.Trim().Equals(id.Trim()));
            var dm = danhmuc.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(sanpham.IdDm.Trim()));
            List<HinhAnhSp> hinhanhsp = _db.HinhAnhSps.Where(x => x.IdSp.Trim().Equals(sanpham.IdSp.Trim())).ToList();

           
            if (dm.TenBang.Trim() == "CHUOT")
            {
                var spchuot = _db.ChuotMayTinhs.FirstOrDefault(x => x.Id.Trim().Equals(sanpham.IdSp.Trim()));
                spthem = ChucNangChung.SPtheoDM(_db, dm.TenBang.Trim()).Take(6).ToList();
                Chuot_SanPhamViewModel chuot = new Chuot_SanPhamViewModel()
                {
                    ChiTiet = spchuot,
                    SanPham = sanpham
                };
                ViewBag.SP = chuot;               
                ViewBag.HA = hinhanhsp;
                ViewBag.DM = dm;
                
            }
            else if (dm.TenBang.Trim() == "BANPHIM") {
                var spchuot = _db.BanPhims.FirstOrDefault(x => x.Id.Trim().Equals(sanpham.IdSp.Trim()));
                spthem = ChucNangChung.SPtheoDM(_db, dm.TenBang.Trim()).Take(6).ToList();
                Banphim_SanPhamViewModel chuot = new Banphim_SanPhamViewModel()
                {
                    ChiTiet = spchuot,
                    SanPham = sanpham
                };
                ViewBag.SP = chuot;              
                ViewBag.HA = hinhanhsp;
                ViewBag.DM = dm;
            }
            else if (dm.TenBang.Trim() == "PCMANHINH") {
                var spchuot = _db.PcManHinhs.FirstOrDefault(x => x.Id.Trim().Equals(sanpham.IdSp.Trim()));
                spthem = ChucNangChung.SPtheoDM(_db, dm.TenBang.Trim()).Take(6).ToList();
                PCManhinh_SanPhamViewModel chuot = new PCManhinh_SanPhamViewModel()
                {
                    ChiTiet = spchuot,
                    SanPham = sanpham
                };
                ViewBag.SP = chuot;               
                ViewBag.HA = hinhanhsp;
                ViewBag.DM = dm;
            }
            else if (dm.TenBang.Trim() == "PCMAYTINHBO") {
                var spchuot = _db.PcMayTinhBos.FirstOrDefault(x => x.Id.Trim().Equals(sanpham.IdSp.Trim()));
                spthem = ChucNangChung.SPtheoDM(_db, dm.TenBang.Trim()).Take(6).ToList();
                PCMaytinhbo_SanPhamViewModel chuot = new PCMaytinhbo_SanPhamViewModel()
                {
                    ChiTiet = spchuot,
                    SanPham = sanpham
                };
                ViewBag.SP = chuot;              
                ViewBag.HA = hinhanhsp;
                ViewBag.DM = dm;
            }
            else if (dm.TenBang.Trim() == "LAPTOP") {
                var spchuot = _db.Laptops.FirstOrDefault(x => x.Id.Trim().Equals(sanpham.IdSp.Trim()));
                spthem = ChucNangChung.SPtheoDM(_db, dm.TenBang.Trim()).Take(6).ToList();
                Laptop_SanPhamViewModel chuot = new Laptop_SanPhamViewModel()
                {
                    ChiTiet = spchuot,
                    SanPham = sanpham
                };
                ViewBag.SP = chuot;           
                ViewBag.HA = hinhanhsp;
                ViewBag.DM = dm;
            }
            else if (dm.TenBang.Trim() == "TAINGHE") {
                var spchuot = _db.TaiNghes.FirstOrDefault(x => x.Id.Trim().Equals(sanpham.IdSp.Trim()));
                spthem = ChucNangChung.SPtheoDM(_db, dm.TenBang.Trim()).Take(6).ToList();
                Tainghe_SanPhamViewModel chuot = new Tainghe_SanPhamViewModel()
                {
                    ChiTiet = spchuot,
                    SanPham = sanpham   
                };
                ViewBag.SP = chuot;               
                ViewBag.HA = hinhanhsp;
                ViewBag.DM = dm;
            }

            return View(spthem);
        }
        public IActionResult ToanBoSP()
        {
            return View();
        }
    }
    

}